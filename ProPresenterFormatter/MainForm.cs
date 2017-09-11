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
            var primitiveText = strings.Where(str => str.Attribute("rvXMLIvarName").Value == "RTFData").Select(el => el.Value).FirstOrDefault();
            if (primitiveText != null)
            {
                var primitiveString = Encoding.UTF8.GetString(Convert.FromBase64String(primitiveText));
                primitiveDisplay.Rtf = primitiveString;
            }
            var flowDocument = ReadFlowDocumentFromXml(strings) ?? CreateFlowDocumentFromRtf(strings);
            XElement stringElement = new XElement("NSString", Convert.ToBase64String(Encoding.UTF8.GetBytes(XamlWriter.Save(flowDocument))));
            stringElement.SetAttributeValue("rvXMLIvarName", "WinFlowData");
            strings.First().Parent.Add(stringElement);
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
                    FontStyle = FontStyles.Normal,
                    FontWeight = (counter % 2 == 0) ? FontWeights.Thin : FontWeights.Regular,
                    FontStretch = (counter % 2 == 0) ? FontStretches.SemiCondensed : FontStretches.Condensed,
                    FontSize = (counter % 2 == 0) ? 65 : 80,
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
            
            for (currentSlide = 0; currentSlide < _groups.Count; currentSlide++)
            {
                LoadSlide();
            }
            _loadedFile.Save(filePathText.Text);
        }
    }
}
