using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Xml.Linq;

namespace ProPresenterFormatter
{
    public partial class MainForm : Form
    {
        private List<XElement> _groups;
        private int currentSlide = 0;
        private XDocument _loadedFile;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _loadedFile = XDocument.Load(openFileDialog.FileName);
                filePathText.Text = openFileDialog.FileName;
                _groups = new List<XElement>(_loadedFile.Root
                    .Descendants("array")
                    .First(el => el.Attribute("rvXMLIvarName").Value=="groups")
                    .Descendants("RVSlideGrouping")
                    .SelectMany(group => group
                        .Descendants("array")
                        .Where(el => el.Attribute("rvXMLIvarName").Value=="slides")
                        .SelectMany(el => el.Descendants("RVDisplaySlide"))));
                LoadSlide();
            }
        }

        private void LoadSlide()
        {
            if (_groups.Count <= currentSlide)
                return;
            var slide = _groups[currentSlide];
            slideDescriptionBox.Text = $"Slide {currentSlide} [name: {slide.Attribute("label")}]";
            var strings = slide.Descendants("array")
                .Where(arr => arr.Attribute("rvXMLIvarName").Value == "displayElements")
                .SelectMany(el => el.Descendants("RVTextElement"))
                .SelectMany(el => el.Descendants("NSString"));
            if (strings.Count() == 0)
                return;
            var primitiveText = strings.Where(str => str.Attribute("rvXMLIvarName").Value == "RTFData").Select(el => el.Value).FirstOrDefault();
            if (primitiveText != null)
            {
                var primitiveString = Encoding.UTF8.GetString(Convert.FromBase64String(primitiveText));
                primitiveDisplay.Rtf = primitiveString;
            }
            var flowDocument = ReadFlowDocumentFromXml(strings) ?? CreateFlowDocumentFromRtf(strings);
            if (!strings.Any(el => el.Attribute("rvXMLIvarName").Value == "WinFlowData"))
            {
                XElement stringElement = new XElement("NSString", Convert.ToBase64String(Encoding.UTF8.GetBytes(XamlWriter.Save(flowDocument))));
                stringElement.SetAttributeValue("rvXMLIvarName", "WinFlowData");
                strings.First().Parent.Add(stringElement);
            }
            // Don't know?
            if (!strings.Any(el => el.Attribute("rvXMLIvarName").Value == "WinFontData"))
            {
                XElement fontData = new XElement("NSString", "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTE2Ij8+PFJWRm9udCB4bWxuczppPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYS1pbnN0YW5jZSIgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLmRhdGFjb250cmFjdC5vcmcvMjAwNC8wNy9Qcm9QcmVzZW50ZXIuQ29tbW9uIj48S2VybmluZz4wPC9LZXJuaW5nPjxMaW5lU3BhY2luZz4wPC9MaW5lU3BhY2luZz48T3V0bGluZUNvbG9yIHhtbG5zOmQycDE9Imh0dHA6Ly9zY2hlbWFzLmRhdGFjb250cmFjdC5vcmcvMjAwNC8wNy9TeXN0ZW0uV2luZG93cy5NZWRpYSI+PGQycDE6QT4yNTU8L2QycDE6QT48ZDJwMTpCPjA8L2QycDE6Qj48ZDJwMTpHPjA8L2QycDE6Rz48ZDJwMTpSPjA8L2QycDE6Uj48ZDJwMTpTY0E+MTwvZDJwMTpTY0E+PGQycDE6U2NCPjA8L2QycDE6U2NCPjxkMnAxOlNjRz4wPC9kMnAxOlNjRz48ZDJwMTpTY1I+MDwvZDJwMTpTY1I+PC9PdXRsaW5lQ29sb3I+PE91dGxpbmVXaWR0aD4xPC9PdXRsaW5lV2lkdGg+PFZhcmlhbnRzPk5vcm1hbDwvVmFyaWFudHM+PC9SVkZvbnQ+");
                fontData.SetAttributeValue("rvXMLIvarName", "WinFontData");
                strings.First().Parent.Add(fontData);
            }
            advancedDisplay.Child = new FlowDocumentReader() { Document = flowDocument };
        }

        private FlowDocument CreateFlowDocumentFromRtf(IEnumerable<XElement> strings)
        {
            var plainText = primitiveDisplay.Text;
            var flowDocument = new FlowDocument() { Background = System.Windows.Media.Brushes.Black };
            int counter = 1;
            foreach (var line in plainText.Split('\n'))
            {
                if (line.Trim() == "")
                    continue;
                Run run = new Run(line)
                {
                    FontFamily = new System.Windows.Media.FontFamily("Flama"),
                    FontWeight = (counter % 2 == 0) ? FontWeights.Thin : FontWeight.FromOpenTypeWeight(650), // Flama has a weird weight
                    FontStretch = (counter % 2 == 0) ? FontStretches.SemiCondensed : FontStretches.SemiCondensed,
                    FontSize = (counter % 2 == 0) ? 80 : 95,
                    Foreground = System.Windows.Media.Brushes.White
                };
                flowDocument.Blocks.Add(new Paragraph(run) { TextAlignment = TextAlignment.Center } );
                counter++;
            }

            return flowDocument;
        }

        private FlowDocument ReadFlowDocumentFromXml(IEnumerable<XElement> strings)
        {
            var complexText = strings.Where(str => str.Attribute("rvXMLIvarName").Value == "WinFlowData").Select(el => el.Value).FirstOrDefault();
            if (complexText != null)
            {
                var complexString = Encoding.UTF8.GetString(Convert.FromBase64String(complexText));
                return XamlReader.Parse(complexString) as FlowDocument;
            }
            return null;
        }

        private void SlideBack(object sender, EventArgs e)
        {
            if (currentSlide > 0)
                currentSlide--;
            LoadSlide();
        }

        private void SlideForward(object sender, EventArgs e)
        {
            currentSlide++;
            LoadSlide();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // walk through every slide to make sure that it's converted

            var temp = currentSlide;

            for (currentSlide = 0; currentSlide < _groups.Count; currentSlide++)
            {
                LoadSlide();
            }
            _loadedFile.Save(filePathText.Text);
            currentSlide = temp;
            LoadSlide();
        }
    }
}
