﻿using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPDM.Interop.epdm;
using System.Windows.Forms;

namespace FishBowl_PDM_BOM_Import_Addin_Official_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region See if All Boxes are Checked, then enable Run Import Button

        private void IsLoggedIntoPDMCheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckForAllBoxes();
        }

        private void NamedCorrectlyCheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckForAllBoxes();
        }

        private void CheckedInCheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckForAllBoxes();
        }

        private void HasAssAndCNCCheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckForAllBoxes();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                IsLoggedIntoPDMCheck.Checked = true;
                NamedCorrectlyCheck.Checked = true;
                CheckedInCheck.Checked = true;
                HasAssAndCNCCheck.Checked = true;
            }
            else
            {
                IsLoggedIntoPDMCheck.Checked = false;
                NamedCorrectlyCheck.Checked = false;
                CheckedInCheck.Checked = false;
                HasAssAndCNCCheck.Checked = false;
            }
        }

        private void CheckForAllBoxes()
        {
            if (IsLoggedIntoPDMCheck.Checked && NamedCorrectlyCheck.Checked && CheckedInCheck.Checked && HasAssAndCNCCheck.Checked)
            {
                RunImport.Enabled = true;
            }
            else
            {
                RunImport.Enabled = false;
            }
        }


        #endregion

        private void RunImport_Click(object sender, EventArgs e)
        {
            bool goodToGo = false;

            if (int.TryParse(PRODBox.Text, out int a) && a != 0)
            {
                if (PRODBox.Text.Length == 4 && DescriptionBox.Text == "") 
                {
                    MessageBox.Show("The description field has to be filled out");
                    return;
                }
                else if (PRODBox.Text.Length != 4 && DescriptionBox.Text != "")
                {
                    MessageBox.Show("The PROD# has to be 4 digits long");
                    return;
                }
                else if (PRODBox.Text.Length != 4 && DescriptionBox.Text == "")
                {
                    MessageBox.Show("The PROD and Description fields need to be filled out");
                    return;
                }
                else
                {
                    if (FBUsername.Text == "" && FBPassword.Text != "")
                    {
                        MessageBox.Show("The FishBowl Username needs to be input");
                        return;
                    }
                    else if (FBUsername.Text != "" && FBPassword.Text == "")
                    {
                        MessageBox.Show("The FishBowl Password needs to be input");
                        return;
                    }
                    else if (FBUsername.Text == "" && FBPassword.Text == "")
                    {
                        MessageBox.Show("FishBowl credentials required");
                        return;
                    }
                    else
                    {
                        goodToGo = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("PROD must be actual numbers, ya donkey");
            }

            if (goodToGo)
            {
                UpdateWindow.Text = "";
                BOMItemsWindow.Text = "";
                RunWholeImport();
            }
        }

        //This is the meat and potatoes of the whole thing:
        private async void RunWholeImport()
        {
            #region Set up initial variables and log into the PDM

            ProgBar.MarqueeAnimationSpeed = 300;
            ProgBar.Maximum = 100;
            ProgBar.Style = ProgressBarStyle.Marquee;

            RunImport.Enabled = false;
            UpdateWindow.Text = "";
            UpdateWindow.Text += "Logging into PDM";

            //For region 1
            bool loggedIn = false;

            IEdmVault21 CurrentVault = new EdmVault5() as IEdmVault21;

            //For region 2
            List<MaterialItem> materialItems = new List<MaterialItem>();
            List<PurchasedItem> purchasedItems = new List<PurchasedItem>();

            //For region 3
            string fullPath;
            string cncPath = null;
            string prodNum = PRODBox.Text;

            try
            {
                CurrentVault.LoginAuto("CreativeWorks", 0);
                loggedIn = true;
            }
            catch
            {
                MessageBox.Show("You lied. You said you were logged into the PDM. Pull yourself together and stop lying.");
                loggedIn = false;
                return;
            }

            #endregion

            #region Substep to make sure there are purchased items, and if not, whether the user wants to continue or not

            if (loggedIn) UpdateWindow.Text += $"\r\nSuccessfully logged into PDM\nSearching for hardware";

            await Task.Run(() => GetHardware(purchasedItems, CurrentVault, prodNum, true));

            List<string> improperlyNamedFiles = new List<string>();

            if (loggedIn && purchasedItems.Count > 0)
            {
                UpdateWindow.Text += $"\r\nFound {purchasedItems.Count} types of hardware: ";
                foreach (var item in purchasedItems)
                {
                    if (item != purchasedItems[0])
                    {
                        if (!item.CheckForProperName(item))
                        {
                            improperlyNamedFiles.Add(item.FileName);
                            BOMItemsWindow.Text += $"\r\n{item.FileName} {item.Quantity}";
                        }
                        else
                        {
                            BOMItemsWindow.Text += $"\r\n{item.PartNo} {item.Quantity}";
                        }
                    }
                    else
                    {
                        if (!item.CheckForProperName(item))
                        {
                            BOMItemsWindow.Text += $"{item.FileName} {item.Quantity}";
                            int index = BOMItemsWindow.Text.IndexOf(item.FileName);
                            int length = item.FileName.Length;
                            BOMItemsWindow.Select(index, length);
                            BOMItemsWindow.SelectionColor = Color.Red;
                        }
                        else
                        {
                            BOMItemsWindow.Text += $"{item.PartNo} {item.Quantity}";
                        }
                    }
                }
            }
            else if (loggedIn && purchasedItems.Count == 0)
            {
                if (MessageBox.Show("Found no hardware or components in the assembly. Continue? Yes", "No", MessageBoxButtons.YesNo) ==
                DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                CheckForAllBoxes();
                ProgBar.MarqueeAnimationSpeed = 0;
                ProgBar.Maximum = 0;
                return;
            }

            for (int i = 0; i < improperlyNamedFiles.Count; i++)
            {
                for (int j = 0; j < purchasedItems.Count; j++)
                {
                    if (improperlyNamedFiles[i] == purchasedItems[j].FileName)
                    {
                        purchasedItems.Remove(purchasedItems[j]);
                        break;
                    }
                }
            }

            #endregion

            #region 3. Use PROD# to find CNC folder and count sheet materials inside

            UpdateWindow.Text += "\r\nGathering material info";

            IEdmSearch9 _search;
            IEdmSearchResult5 _searchResult;

            _search = (IEdmSearch9)CurrentVault.CreateSearch2();
            _search.FindFiles = true;

            await Task.Run(() =>
            {
                _search.Clear();
                _search.StartFolderID = CurrentVault.GetFolderFromPath("C:\\CreativeWorks").ID;

                //_search.AddVariable("DocumentNumber", prodNum);
                _search.FileName = "PROD-" + prodNum + ".SLDASM";

                _search.GetFirstResult();

                bool exceptionEncountered = false;

                if (exceptionEncountered)
                {
                    _searchResult = null;
                }
                else
                {
                    _searchResult = _search.GetFirstResult();
                }

                if (_searchResult == null)
                {
                    MessageBox.Show("Didn't find anything for the provided PROD number");
                    return;
                }
                else
                {
                    fullPath = _searchResult.Path;
                    cncPath = GetCncPath(fullPath);
                }

                List<Laminate> laminates = new List<Laminate>();

                if (cncPath.Contains("LP\\GRP"))
                {
                    LaminateForm laminateForm = new LaminateForm();
                    if (laminateForm.ShowDialog() == DialogResult.OK) laminates = laminateForm.laminates;
                }

                if (laminates.Count > 0)
                {
                    foreach (var laminate in laminates)
                    {
                        materialItems.Add(new MaterialItem(laminate.Type, laminate.Quantity, GetMaterialPartNo(laminate.Type)));
                    }
                }

                _search.Clear();
                _search.StartFolderID = CurrentVault.GetFolderFromPath(cncPath).ID;

                _search.FindFiles = true;

                _search.FileName = ".sbp";

                _searchResult = _search.GetFirstResult();

                string tempPartNo = null;
                string materialName;

                List<string> partNumbers = new();

                if (_searchResult != null && !_searchResult.Path.Contains("Parts"))
                {
                    materialName = GetMaterialName(_searchResult.Name);
                    partNumbers.Add(GetMaterialPartNo(materialName));
                    materialItems.Add(new MaterialItem(materialName, 1, partNumbers[0]));
                }

                do
                {
                    _searchResult = _search.GetNextResult();

                    if (_searchResult != null && _searchResult.Path.Contains("Programs") && !_searchResult.Path.Contains("Parts") && !_searchResult.Path.Contains("Laminate") && !IsMultiPart(_searchResult.Name))
                    {
                        materialName = GetMaterialName(_searchResult.Name);
                        tempPartNo = GetMaterialPartNo(materialName);

                        bool found = false;
                        foreach (var number in partNumbers)
                        {
                            if (number == tempPartNo)
                            {
                                for (int i = 0; i < materialItems.Count; i++)
                                {
                                    if (materialItems[i].PartNo == number)
                                    {
                                        materialItems[i].Quantity++;
                                        found = true;
                                        break;
                                    }
                                }
                            }
                            if (found) break;
                        }
                        if (!found)
                        {
                            partNumbers.Add(tempPartNo);
                            materialItems.Add(new MaterialItem(materialName, 1, tempPartNo));
                        }
                    }
                    else
                    {
                        continue;
                    }
                } while (_searchResult != null);
            });

            if (materialItems.Count > 0)
            {
                CheckForDuplicates(materialItems);
                UpdateWindow.Text += $"\r\nFound {materialItems.Count - DuplicateMaterialCount(materialItems)} types of materials: ";
                foreach (var material in materialItems)
                {
                    if (material.PartNo != "0001") BOMItemsWindow.Text += $"\r\n{material.PartNo} {material.Quantity}";
                }

                for (int i = 0; i < materialItems.Count; i++)
                {
                    if (materialItems[i].PartNo == "0001")
                    {
                        materialItems.RemoveAt(i);
                        i--;
                    }
                }
            }

            #endregion

            #region 4. Connect to FishBowl

            UpdateWindow.Text += "\nConnecting to FishBowl, please wait";

            List<string[]> parts = new List<string[]>();

            Fishbowl fishbowl = new Fishbowl(FBServer.Text, Convert.ToInt32(FBPort.Text), FBUsername.Text, FBPassword.Text);

            int? statusCode = await Task.Run(() => ConnectWithFishBowl(fishbowl));

            if (statusCode != null)
            {
                UpdateWindow.Text += $"\r\nStatus Code: {statusCode}: {TranslateStatusCode(statusCode)}";
            }
            else
            {
                UpdateWindow.Text += $"\r\nStatus Code: {statusCode}: {TranslateStatusCode(statusCode)}";
            }

            #endregion

            #region 5. Run query on parts in FishBowl

            UpdateWindow.Text += "\r\nRunning query in FB";

            string query = await Task.Run(() => ExecuteQueryInFishBowl(fishbowl));

            string[] indParts = query.Split(new string[] { "\r\n " }, StringSplitOptions.RemoveEmptyEntries);

            //string table = fishbowl.ConvertDataTableToCsv(query);
            if (query == "")
            {
                Console.WriteLine("Returned nothing from the query");
            }
            else
            {
                foreach (string part in indParts)
                {
                    parts.Add(part.Trim().Trim('"').Split("\",\""));
                }

                #endregion

                #region 6. Compare items found in the PDM to the FishBowl query

                List<VerifiedFishBowlItem> verifiedItems = new List<VerifiedFishBowlItem>();

                //I guess we're brute forcing this shit.
                foreach (var part in parts)
                {
                    if (purchasedItems.Count != 0)
                    {
                        foreach (var purchasedItem in purchasedItems)
                        {
                            if (part[0] == purchasedItem.PartNo)
                            {
                                verifiedItems.Add(new VerifiedFishBowlItem(part[0], part[1], purchasedItem.Quantity, "ea"));
                                purchasedItems.Remove(purchasedItem);
                                break;
                            }
                        }
                    }
                }

                foreach (var part in parts)
                {
                    if (materialItems.Count != 0)
                    {
                        foreach (var materialItem in materialItems)
                        {
                            if (part[0] == materialItem.PartNo)
                            {
                                if (part[0] == "6041" || part[0] == "6040" || part[0] == "0422442")
                                {
                                    verifiedItems.Add(new VerifiedFishBowlItem(part[0], part[1], materialItem.Quantity, "ea"));
                                    materialItems.Remove(materialItem);
                                    break;
                                }
                                else
                                {
                                    verifiedItems.Add(new VerifiedFishBowlItem(part[0], part[1], materialItem.Quantity, "SHT"));
                                    materialItems.Remove(materialItem);
                                    break;
                                }
                            }
                        }
                    }
                }

                if (materialItems.Count != 0 || purchasedItems.Count != 0)
                {
                    UpdateWindow.Text += "\r\nSome items were not found in FB";
                    BOMItemsWindow.Text += "\r\n\r\nParts not found in FishBowl: ";
                    if (materialItems.Count != 0)
                    {
                        foreach (var material in materialItems)
                        {
                            BOMItemsWindow.Text += $"\r\n{material.MaterialName}";
                        }
                    }
                    else if (purchasedItems.Count != 0)
                    {
                        foreach (var item in purchasedItems)
                        {
                            BOMItemsWindow.Text += $"\r\n{item.FileName}";
                        }
                    }

                    BOMItemsWindow.Text += "\r\n\r\nParts found in FishBowl: ";
                    foreach (var part in verifiedItems)
                    {
                        BOMItemsWindow.Text += $"\r\n{part.PartNo} {part.Description} {part.Quantity} {part.UOM}";
                    }
                }
                else
                {
                    UpdateWindow.Text += "\r\nAll parts found in FB";
                    foreach (var part in verifiedItems)
                    {
                        BOMItemsWindow.Text += $"\r\n\r\n{part.PartNo} {part.Description} {part.Quantity} {part.UOM}";
                    }
                }

                #endregion

                #region 7. Run the import

                UpdateWindow.Text += "\r\nRunning Import, please wait";

                prodNum = "PROD-" + prodNum;

                List<string> import = new List<string>();
                import.Add(@"""Flag"",""Number"",""Description"",""Active"",""Revision"",""CalendarCategory"",""AutoCreateType"",""QuickBooksClassName""");
                import.Add($"\"BOM\",\"{prodNum}\",\"{DescriptionBox.Text}\",\"True\",1,\"Order\",\"Never\",,");

                //"BOM'"


                //import.Add(@"""Flag"",""Description"",""Type"",""Part"",""Quantity"",""UOM""");
                foreach (var item in verifiedItems)
                {
                    import.Add($"\"Item\",\"{DealWithDoubleQuotes(item.Description)}\",\"Raw Good\",\"{item.PartNo}\",{item.Quantity},\"{item.UOM}\",,,,,,,,,,,");
                }

                //There has to be at least one raw good and one finished good
                import.Add($"\"Item\",\"{prodNum}\",\"Finished Good\",\"{prodNum}\",1,\"ea\",,,,,,,,,,,");

                string bomImportStatus = await Task.Run(() => RunImportInFishBowl(fishbowl, import));

                if (int.TryParse(bomImportStatus, out int a))
                {
                    UpdateWindow.Text += $"\r\nImport Status: {a}... That means it worked.";
                    UpdateWindow.Text += "\r\nHave a good day.";
                }
                else
                {
                    UpdateWindow.Text += $"\r\nSomething went weird somewhere. Check this out:\r\n{bomImportStatus}";
                }

                #endregion

                #region Finish everything up

                UpdateWindow.Text += "\r\nLogging out of FishBowl...";

                //Log out of FishBowl
                await Task.Run(() => fishbowl.Dispose());

                ProgBar.Value = 0;
                ProgBar.MarqueeAnimationSpeed = 0;

                #endregion

                foreach (var file in improperlyNamedFiles)
                {
                    WrongNamesBox.Text += $"\r\n{file}";

                    int index = BOMItemsWindow.Text.IndexOf(file);
                    int length = file.Length;
                    BOMItemsWindow.Select(index, length);
                    BOMItemsWindow.SelectionColor = Color.Red;
                }
            }
        }

        #region Methods

        private static void GetHardware(List<PurchasedItem> purchasedItems, IEdmVault21 CurrentVault, string newProdNum, bool isProd)
        {
            IEdmFile5 File;
            IEdmReference5 Reference;
            IEdmReference10 @ref;

            IEdmSearch9 _search;
            IEdmSearchResult5 _searchResult;

            _search = (IEdmSearch9)CurrentVault.CreateSearch2();
            _search.FindFiles = true;

            _search.Clear();
            _search.StartFolderID = CurrentVault.GetFolderFromPath("C:\\CreativeWorks").ID;

            //_search.AddVariable("DocumentNumber", prodNum);
            if (isProd) _search.FileName = "PROD-" + newProdNum + ".SLDASM";
            else _search.FileName = newProdNum + ".SLDASM";

            _search.GetFirstResult();

            bool exceptionEncountered = false;

            if (exceptionEncountered)
            {
                _searchResult = null;
            }
            else
            {
                _searchResult = _search.GetFirstResult();
            }

            if (_searchResult == null)
            {
                MessageBox.Show($"Returned nothing for PROD-{newProdNum}");
                return;
            }

            File = CurrentVault.GetFileFromPath(_searchResult.Path, out IEdmFolder5 ParentFolder);

            Reference = (IEdmReference10)File.GetReferenceTree(ParentFolder.ID);

            IEdmPos5 pos = Reference.GetFirstChildPosition("Get Some", true, true, 0);
            //Console.WriteLine(pos.ToString());x 

            string[] containsTerms = new string[] { "SB", "MFAB", "BEAU 25 PC Post", "3D Printed", "Multibody" };

            while (!pos.IsNull)
            {
                @ref = (IEdmReference10)Reference.GetNextChild(pos);
                if (!CheckForIgnorableTerms(@ref, containsTerms))
                {
                    if (@ref.Name.Contains("SLDASM"))
                    {
                        newProdNum = GetProdNum(@ref.Name);
                        isProd = newProdNum.ToUpper().Contains("PROD") ? true : false;
                        GetHardware(purchasedItems, CurrentVault, newProdNum, isProd);
                    }
                    else
                    {
                        if (!@ref.Name.ToUpper().Contains("PROD"))
                        {
                            purchasedItems.Add(new PurchasedItem(@ref.Name, GetPartNo(@ref.Name), @ref.RefCount));
                        }
                    }
                }
            }
        }
        private static string GetProdNum(string name)
        {
            name = name.Replace("PROD-", "");
            name = name.Replace(".SLDASM", "");
            return name;
        }
        private static string GetCncPath(string fullPath)
        {
            string cutPattern = @"1-Design Reference.+";
            string cncPath = Regex.Replace(fullPath, cutPattern, "");
            cncPath += "2-CNC";
            return cncPath;
        }
        private static string GetPartNo(string name)
        {
            string partNoPattern1 = @".SLDPRT";
            string partNoPattern2 = @"[a-zA-Z0-9-\s#*()]+$";
            string partNo = Regex.Replace(name, partNoPattern1, "");
            partNo = Regex.Match(partNo, partNoPattern2).ToString();
            return partNo;
        }
        private static string GetMaterialPartNo(string temp)
        {
            switch (temp)
            {
                case "Xanadu":
                    return "7945 - 18 - 350";
                case "Walnut Heights":
                    return "7965 - 12 - 350";
                case "Violine":
                    return "S-3055 - T - NF39";
                case "Tutti Frutti":
                    return "Y0026-60 - 372";
                case "Pewter":
                    return "D73-60 - 350";
                case "Pearl Weave":
                    return "5332";
                case "Passionfruit":
                    return "SR5240-T - NF39";
                case "Oiled Soapstone":
                    return "4882 - 38 - 350";
                case "Midnight":
                    return "D505-60 - 350";
                case "Lemon Lime":
                    return "Y0359-60 - 372";
                case "Honey Plantain":
                    return "S-4025 - T - NF39";
                case "Hollyberry":
                    return "d307 - 60 - 350";
                case "Hibiscus Tea":
                    return "Y0334-60 - 372";
                case "Hibiscus":
                    return "9224";
                case "Classic Marbleized":
                    return "Y0673-38 - 372";
                case "Carribean Blue":
                    return "S-3070 - T - NF39";
                case "Capri":
                    return "9225";
                case "Brighton Walnut":
                    return "7922 - 07 - 350";
                case "Barrel Herringbone":
                    return "y0591 - 60 - 372";
                case "Celtec-black 50":
                    return "12760";
                case "1-MDX 50":
                    return "0214003";
                case "1-MDX 50 no Margin":
                    return "0214003";
                case "2-MDX 75":
                    return "222003";
                case "3-Cheap Plywood 18mm (.7) ":
                    return "0422442";
                case "NO MARGIN - Cheap Plywood 18mm (.7)":
                    return "0422442";
                case "3mm Laminate":
                    return "Find out which laminate";
                case "4- Birch Plywood 18mm (.7)":
                    return "0622133";
                case "ABS-black 125":
                    return "173144";
                case "ABS-white 125":
                    return "WHITEABS";
                case "Acrylic-clear (Impact Resistant) 25":
                    return "1/4\" Super_Impact Resistant acrylic";
                case "Acrylic-clear 25":
                    return "220958";
                case "Acrylic-clear 375":
                    return "16681";
                case "Acrylic-clear 50":
                    return "244373";
                case "Acrylic-mirrored 125":
                    return "18266";
                case "Acrylic-orange 125":
                    return "ACRY21190";
                case "Acrylic-orange 25":
                    return "ACS-00220ORG000001";
                case "Bendaply 375 (Hamburg)":
                    return "6041";
                case "Plywood 375 bendaply (Grain)":
                    return "6041";
                case "Bendaply 375 (Hotdog)":
                    return "6040";
                case "Celtec-black 75":
                    return "15715";
                case "Celtec-white 25":
                    return "13428";
                case "Celtec-white 50":
                    return "208138";
                case "Celtec-white 75":
                    return "208137";
                case "ColorCore Blue/White/Blue 75":
                    return "36916";
                case "Dibond 125":
                    return "184514";
                case "HDPE-white 0625":
                    return "18096";
                case "HDPE-white 125":
                    return "111717";
                case "HDPE-white 25":
                    return "111723";
                case "HDPE-white 75":
                    return "39175";
                case "MDF 25":
                    return "3908531";
                case "MDF 25 no Margin":
                    return "3908531";
                case "MDF 25 Stencil Board":
                    return "3908531";
                case "MDF 50":
                    return "MDF1248";
                case "MDF 75":
                    return "MDF 3/4";
                case "MDF 75 for 10' Board":
                    return "MDF 3/4";
                case "MDX 75 NO MARGIN":
                    return "222003";
                case "Melamine-black 18mm":
                    return "922133";
                case "Particle 6875":
                    return "8022539";
                case "PETG 0625":
                    return "18928";
                case "Plywood 6875":
                    return "18MMBirchAetna";
                case "Plywood 6875 (Grain)":
                    return "18MMBirchAetna";
                case "Seaboard-black 50":
                    return "33770";
                case "Seaboard-black 75":
                    return "25979";
                case "Diffusion Film 0325":
                    return "99078415";
                default:
                    return "N/A";
            }
        }
        private static string GetMaterialName(string name)
        {
            string frontEndPattern = @"^[0-9]+_";
            string backEndPattern = @"[0-9].sbp";

            string materialName = Regex.Replace(name, frontEndPattern, "");
            materialName = Regex.Replace(materialName, backEndPattern, "");

            return materialName;
        }
        private static bool IsMultiPart(string name)
        {
            string pattern = @"[0-9]\.sbp";
            string checkName = Regex.Match(name, pattern).ToString();

            if (checkName != null)
            {
                if (checkName.StartsWith('1'))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }
        static void CheckForDuplicates(List<MaterialItem> materialItems)
        {
            for (int i = 0; i < materialItems.Count - 1; i++)
            {
                for (int j = i; j < materialItems.Count; j++)
                {
                    if (i != j)
                    {
                        if (materialItems[i].PartNo == materialItems[j].PartNo)
                        {
                            materialItems[i].Quantity++;
                            materialItems[j].PartNo = "0001";
                        }
                    }
                }
            }
        }
        static int DuplicateMaterialCount(List<MaterialItem> materialItems)
        {
            int count = 0;
            foreach (var item in materialItems)
            {
                if (item.PartNo == "0001") count++;
            }
            return count;
        }
        private static int? ConnectWithFishBowl(Fishbowl fishbowl)
        {
            int? statusCode = fishbowl.Connect();
            return statusCode;
        }
        private static string ExecuteQueryInFishBowl(Fishbowl fishbowl)
        {
            string result = fishbowl.ExecuteQuery(query: "SELECT part.num AS Number, part.Description AS Description FROM part");
            return result;
        }
        private static string RunImportInFishBowl(Fishbowl fishbowl, List<string> import)
        {
            string importResult = fishbowl.Import("ImportBillOfMaterials", import);
            return importResult;
        }
        static string TranslateStatusCode(int? statusCode)
        {
            return statusCode switch
            {
                1000 => "Success!",
                _ => "Something didn't work right.",
            };
        }
        private static string DealWithDoubleQuotes(string description)
        {
            string[] split = new string[2];
            string result = description;
            int quoteCount = 1;

            for (int i = 1; i < description.Length - 1; i++)
            {
                if (description[i] == '"')
                {
                    split[0] = result.Substring(0, i + quoteCount);
                    split[1] = result.Substring(i + quoteCount);
                    split[0] = split[0] + "\"";
                    result = split[0] + split[1];
                    quoteCount++;
                }
            }
            return result;
        }
        private static bool CheckForIgnorableTerms(IEdmReference10 @ref, string[] containsTerms)
        {
            foreach(var term in containsTerms)
            {
                if (@ref.Name.ToUpper().Contains(term.ToUpper())) return true;
            }
            if (@ref.Name[@ref.Name.Length - 1] == '-') return true;
            return false;
        }

        #endregion
    }
}
