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
using System.IO;

namespace CarsTVGProfileEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Races> racesList = new List<Races>();
        private List<MiniGames> minigamesList = new List<MiniGames>();
        private string[] saveDirs;
        public const int profileNameLengthInBytes = 7;
        private byte[] selectedSaveFileInBytes;
        private byte[] headerFileInBytes;
        public MainWindow()
        {
            InitializeComponent();
            SaveDirsCombobox.SelectionChanged += SaveDirsCombobox_SelectionChanged;
            racesList = Races.MakeClearRacesList();

            saveDirs = Directory.GetDirectories($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\THQ\\Cars\\Save");

            foreach (var directory in saveDirs)
            {
                if (Directory.GetFiles(directory).Length != 0)
                {
                    string dirName = System.IO.Path.GetFileName(directory);
                    SaveDirsCombobox.Items.Add(dirName);
                }
            }
            SaveDirsCombobox.SelectedIndex = 0;
            foreach (Races race in racesList)
            {
                EventNamesCombobox.Items.Add(race.RaceName);
            }
            EventNamesCombobox.SelectedIndex = 0;

        }

        private void SaveDirsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!File.Exists($"{saveDirs[SaveDirsCombobox.SelectedIndex]}\\Data.dat"))
            {
                MessageBox.Show("Unable to find Data.dat file of selected profile! Check if it exists and try again, otherwise it might've been corrupted!");
            }
            if (!File.Exists($"{saveDirs[SaveDirsCombobox.SelectedIndex]}\\Header.dat"))
            {
                MessageBox.Show("Unable to find Headedr.dat file of selected profile! Check if it exists and try again, otherwise it might've been corrupted!");
            }
            selectedSaveFileInBytes = File.ReadAllBytes($"{saveDirs[SaveDirsCombobox.SelectedIndex]}\\Data.dat");
            headerFileInBytes = File.ReadAllBytes($"{saveDirs[SaveDirsCombobox.SelectedIndex]}\\Header.dat");
            ChangeLicensePlateName(headerFileInBytes);
            racesList = Races.LoadRacesFromSave(racesList, selectedSaveFileInBytes);
            EventNamesCombobox.SelectedIndex = 0;
        }
        private void EventNamesCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (racesList[EventNamesCombobox.SelectedIndex].RaceState)
            {
                case Races.eventState.Locked:
                    LockedRB.IsChecked = true;
                    break;
                case Races.eventState.UnlockedInArcadeInvisibleInStoryMode:
                    UnlockedInArcadeInvisibleInStoryModeRB.IsChecked = true;
                    break;
                case Races.eventState.UnplayedUnlocked:
                    UnplayedUnlockedRB.IsChecked = true;   
                    break;
                case Races.eventState.Unplayed:
                    UnplayedRB.IsChecked= true;
                    break;
                case Races.eventState.CompletedNoTropiesAquired:
                    CompletedNoTrophiesRB.IsChecked = true;
                    break;
                case Races.eventState.CompletedAllTrophiesAquired:
                    CompletedAllTrophiesRB.IsChecked = true;
                    break;
            }
        }

        private void ChangeLicensePlateName(byte[] headerInBytes)
        {
            string nameWithNoEmptySpaces = string.Empty;
            byte[] profileName = new byte[profileNameLengthInBytes];
            for (int i = 0; i < profileName.Length; i++)
            {
                profileName[i] = headerInBytes[i];
            }
            LicensePlateTextBox.Text = BytesArrayToString(profileName);
            for (int i = LicensePlateTextBox.Text.Length - 1; i >= 0; i--)
            {
                //done due to profiles might have spaces inbetween characaters 
                if (LicensePlateTextBox.Text[i] != '\0')
                {
                    for (int j = i; j >= 0; j--)
                    {
                        nameWithNoEmptySpaces += LicensePlateTextBox.Text[j];
                    }
                    LicensePlateTextBox.Text = string.Empty;
                    for (int k = nameWithNoEmptySpaces.Length - 1; k >= 0; k--)
                    {
                        LicensePlateTextBox.Text += nameWithNoEmptySpaces[k];
                    }
                    return;
                }
            }
            LicensePlateTextBox.Text = nameWithNoEmptySpaces;
        }

        public string BytesArrayToString(byte[] stringInBytes)
        {
            return System.Text.Encoding.ASCII.GetString(stringInBytes);
        }

        public byte[] StringToByteArray(string profileName, int byteSize)
        {
            byte[] headerInBytes = new byte[byteSize];
            byte[] temp = new byte[byteSize];
            headerInBytes = Encoding.ASCII.GetBytes(profileName);
            if(headerInBytes.Length == 7)
            {
                return headerInBytes;
            }
            for (int i = 0; i < headerInBytes.Length; i++)
            {
                temp[i] = headerInBytes[i];
            }
            return temp;
        }

        private void ApplyChangesToProfileName(string newProfileName)
        {
            byte[] profileNameInBytes = StringToByteArray(newProfileName, profileNameLengthInBytes);


            //replacing characters of first 7 bytes in Header and Data files cuz otherwise it breaks those files
            for (int i = 0; i < profileNameInBytes.Length; i++)
            {
                headerFileInBytes[i] = profileNameInBytes[i];
                selectedSaveFileInBytes[i] = profileNameInBytes[i];
            }
            try
            {
                File.WriteAllText($"{saveDirs[SaveDirsCombobox.SelectedIndex]}\\Header.dat", string.Empty);
                File.WriteAllBytes($"{saveDirs[SaveDirsCombobox.SelectedIndex]}\\Header.dat", headerFileInBytes);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong! Try again!");
            }
            MessageBox.Show("Your profile has been succesfully saved!");


        }
        private void ApplyChangesToRacesStatus(List<Races> changedRacesList)
        {
            //replacing existing races status to new ones choosen by user
            for (int i = 0; i < racesList.Count; i++)
            {
                selectedSaveFileInBytes[racesList[i].RaceId] = (byte)racesList[i].RaceState;
            }
            try
            {
                File.WriteAllText($"{saveDirs[SaveDirsCombobox.SelectedIndex]}\\Data.dat", string.Empty);
                File.WriteAllBytes($"{saveDirs[SaveDirsCombobox.SelectedIndex]}\\Data.dat", selectedSaveFileInBytes);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong! Try again!");
            }
            MessageBox.Show("Your changes has been succesfully saved!");

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyChangesToProfileName(LicensePlateTextBox.Text);
            ApplyChangesToRacesStatus(racesList);
        }

        private void RaceStateRB_Checked(object sender, RoutedEventArgs e)
        {
            switch ((e.Source as RadioButton).Content.ToString())
            {
                case "Locked":
                    racesList[EventNamesCombobox.SelectedIndex].RaceState = Races.eventState.Locked;
                    break;
                case "Unlocked In Arcade (Not in Story Mode)":
                    racesList[EventNamesCombobox.SelectedIndex].RaceState = Races.eventState.UnlockedInArcadeInvisibleInStoryMode;
                    break;
                case "Unplayed unlocked":
                    racesList[EventNamesCombobox.SelectedIndex].RaceState = Races.eventState.UnplayedUnlocked;
                    break;
                case "Unplayed":
                    racesList[EventNamesCombobox.SelectedIndex].RaceState = Races.eventState.Unplayed;
                    break;
                case "Completed (No trophies aquired)":
                    racesList[EventNamesCombobox.SelectedIndex].RaceState = Races.eventState.CompletedNoTropiesAquired;
                    break;
                case "Completed (All trophies aquired)":
                    racesList[EventNamesCombobox.SelectedIndex].RaceState = Races.eventState.CompletedAllTrophiesAquired;
                    break;
            }

        }
    }
}
