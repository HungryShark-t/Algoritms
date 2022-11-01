using System;
using System.Collections.Generic;
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
using System.Xml.Linq;
using Jurnal;

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для Reception.xaml
    /// </summary>
    public partial class Reception : Window
    {
        List<Patient> pl { get; set; }
        //List<string> Special { get; set; }
        public string surname { get; set; }
        public int age { get; set; }
        public string address { get; set; }
        public int number { get; set; }
        public string specialist { get; set; }

        public Reception()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Patient_Loaded);
        }

        //Загрузка данных
        void Patient_Loaded(object sender, RoutedEventArgs e)
        {
            ReadXML();
            Patient.ItemsSource = pl;
            //Spec.ItemsSource = Special;
        }

        public void ReadXML()
        {
            XDocument xDoc = XDocument.Load("../../Patient.xml");
            pl = new List<Patient>();
            //Special = new List<string>();
            foreach (XElement xnode in xDoc.Root.Elements())
            {

                //Special.Add(xnode.Element("specialist").Value);
                surname = xnode.Attribute("surname").Value;


                foreach (XElement dot in xnode.Elements())
                {

                    if ("age".Equals(dot.Name.ToString()))
                    {
                        age = Int32.Parse(dot.Value);
                    }

                    if ("address".Equals(dot.Name.ToString()))
                    {
                        address = dot.Value;
                    }
                    if ("number".Equals(dot.Name.ToString()))
                    {
                        number = Int32.Parse(dot.Value);
                    }
                    if ("specialist".Equals(dot.Name.ToString()))
                    {
                        specialist = dot.Value;
                    }
                    
                }
                pl.Add(new Patient(surname, age, address, number, specialist));
            }

        }

        //Фильтр на поиск по фио
        private bool PatientFilter(object item)
        {
            if (String.IsNullOrEmpty(textBox.Text))
                return true;
            else
                return ((item as Patient).Surname.IndexOf(textBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        //Изменение таблицы 
        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Patient.ItemsSource).Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Patient.ItemsSource);
            view.Filter = PatientFilter;
        }


        private void Patient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Patient itemToRemove = (Patient)((ListBox)sender).SelectedItem;
            
            string sMessageBoxText = "Данный пациент будет удален из списка. Хотите продолжить?";
            string sCaption = "Удаление пациента";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult wrs = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (wrs)
            {
                case MessageBoxResult.Yes:
                    this.pl.Remove(itemToRemove); // удаление
                    CollectionViewSource.GetDefaultView(Patient.ItemsSource).Refresh();
                    break;

                case MessageBoxResult.No:
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddPatient add = new AddPatient(pl);
            add.Show();
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Patient.ItemsSource);
            //view.Filter = PatientFilter;
            //CollectionViewSource.GetDefaultView(Patient.ItemsSource).Refresh();
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Patient.ItemsSource);
            view.Filter = PatientFilter;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //MainWindow mw = new MainWindow();
           // mw.Show();
            this.Close();
        }
    }
}
