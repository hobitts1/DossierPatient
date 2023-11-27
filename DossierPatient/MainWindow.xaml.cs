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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DossierPatient
{ /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
    public partial class MainWindow : Window
    {
        DatabasePatientEntities1 patientEntities;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //patientEntities = (DatabasePatientEntities)DataContext;
            patientEntities = new DatabasePatientEntities1();
            cboPatient.DataContext = patientEntities.Patients.ToList();

        }





        private void cboPatient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lsDossierMedicals.DataContext = ((Patient)cboPatient.SelectedItem).DossierMedicals;
        }

        private void btnAjouterPatient_Click(object sender, RoutedEventArgs e)
        {
            DossierMedical dossier1 = new DossierMedical();
            dossier1.IdPatient = 33333;
            dossier1.Diagnostique = "ulcer gastrique";
            dossier1.IdDossierMedical = 45892;
            dossier1.Etat = "actife";

            patientEntities.DossierMedicals.Add(dossier1);
            patientEntities.SaveChanges();
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            DossierMedical dossier1 = ((DossierMedical)lsDossierMedicals.SelectedItem);
            patientEntities.DossierMedicals.Remove(dossier1);
            patientEntities.SaveChanges();
        }

        private void btnDiagnostiqueActive_Click(object sender, RoutedEventArgs e)
        {
            DossierMedical dossier1 = (DossierMedical)lsDossierMedicals.SelectedItem;
            dossier1.Etat = "Active";
            patientEntities.SaveChanges();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                List<DossierMedical> dossiersActives = (from c in ((Patient)cboPatient.SelectedItem).
                                                        DossierMedicals
                                                        where c.Etat == "active"
                                                        select c).ToList();
                lsDossierMedicals.DataContext = dossiersActives;
            }
            else
            {
                lsDossierMedicals = (ListBox)((Patient)cboPatient.SelectedItem).DossierMedicals;
            }
        }
    }
}
