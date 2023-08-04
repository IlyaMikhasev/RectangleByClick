using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace RectangleByClick
{
    [Serializable]
    public  class Figure
    {
        //private Rectangle _rectangle;
        [XmlAttribute]
        public Rectangle rectangle { get; }
        //private Brush _brush;
        [XmlAttribute]
        public Brush brush { get; }
        public Figure() { }
        public Figure(Rectangle rectangle, Brush brush)
        {
            this.rectangle = rectangle;
            this.brush = brush;
        }
        static public void Serealize_it(string filename, List<Figure> objectGrath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Figure>));
            using (Stream fStream = new FileStream(filename,
                FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fStream, objectGrath);
            }
        }
        static public void Deserealize_it(string filename, out List<Figure> lst)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Figure>));
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    lst = (List<Figure>)xmlSerializer.Deserialize(reader);
                }
            }
            catch
            {
                lst = new List<Figure>();
            }
        }
    }
}
