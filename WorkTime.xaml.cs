using Doctors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для WorkTime.xaml
    /// </summary>
    public partial class WorkTime : Window
    {
        List<Doctor> dl { get; set; }
        List<string> Special { get; set; }
        public string name { get; set; }
        public string specialist { get; set; }
        public string cabinet { get; set; }
        public string pn { get; set; }
        public string vt { get; set; }
        public string sr { get; set; }
        public string ch { get; set; }
        public string pt { get; set; }

        // Doctors_list dl;
        public WorkTime()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(WorkTime_Loaded);

        }

        //Загрузка данных
        void WorkTime_Loaded(object sender, RoutedEventArgs e)
        {
            ReadXML();
            Doctor.ItemsSource = dl;
            Spec.ItemsSource = Special;
        }

        public void ReadXML()
        {
            XDocument xDoc = XDocument.Load("../../Main.xml");
            dl = new List<Doctor>();
            Special = new List<string>();
            foreach (XElement xnode in xDoc.Root.Elements())
            {

                Special.Add(xnode.Element("specialist").Value);
                name = xnode.Attribute("name").Value;
                
                
                foreach (XElement dot in xnode.Elements())
                {
           
                    if ("specialist".Equals(dot.Name.ToString()))
                    {
                       specialist = dot.Value ;                       
                    }

                    if ("cabinet".Equals(dot.Name.ToString()))
                    {
                       cabinet = dot.Value;
                    }
                    if ("pn".Equals(dot.Name.ToString()))
                    {
                        pn = dot.Value;
                    }
                    if ("vt".Equals(dot.Name.ToString()))
                    {
                        vt = dot.Value;
                    }
                    if ("sr".Equals(dot.Name.ToString()))
                    {
                        sr = dot.Value;
                    }
                    if ("ch".Equals(dot.Name.ToString()))
                    {
                        ch = dot.Value;
                    }
                    if ("pt".Equals(dot.Name.ToString()))
                    {
                        pt = dot.Value;
                    }
                }
                dl.Add(new Doctor(name, specialist, cabinet, pn, vt, sr, ch, pt));
            }
           
        }


        //Фильтр на поиск врача по фио
        private bool DoctorFilter(object item)
        {
            if (String.IsNullOrEmpty(textBox.Text))
                return true;
            else
                return ((item as Doctor).Name.IndexOf(textBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        //Изменение таблицы 
        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Doctor.ItemsSource).Refresh();
        }

        //Поиск
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Doctor.ItemsSource);
            view.Filter = DoctorFilter;
        }

        //Поиск врача по его специальности
        public bool comboFilter(Object item)
        {
            string sl = (string)Spec.SelectedItem;
           
            Doctor specia = (Doctor)item;
            return specia.Specialist == sl;
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICollectionView look = CollectionViewSource.GetDefaultView(dl);
            look.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
             ICollectionView cm = CollectionViewSource.GetDefaultView(dl);
             cm.Filter = new Predicate<object>(comboFilter);
             textBox.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


}

       
    

