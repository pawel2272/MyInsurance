using CsvHelper;
using Microsoft.Win32;
using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyInsurance.BusinessLogic.Services
{
    /// <summary>
    /// Klasa <c>FileManager<typeparamref name="T"/> służy do zarządzania plikami.</c>
    /// </summary>
    /// <typeparam name="T">Może nim być klasa Appointment, Patient lub Employee.</typeparam>
    public class FileService<T>
    {
        /// <summary>
        /// Zawiera informację o aktualnie otwartym pliku.
        /// </summary>
        private FileInfo CurrentFile;

        public bool SavedLastTime()
        {
            if (CurrentFile != null)
            {
                if (!CurrentFile.FullName.Equals(string.Empty))
                {
                    return true;
                }    
            }
            return false;
        }

        /// <summary>
        /// Metoda Open klasy FileManager<typeparamref name="T"/>.
        /// </summary>
        /// <param name="listOfObjects">Kolekcja, do której ma być dodany obiekt wczytany z pliku.</param>
        /// <remarks>
        /// Wczytuje obiekt zapisany w pliku .csv.
        /// </remarks>
        public void Open(CommonDbService dbService)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Plik *.csv|*.csv;";
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader streamReader = File.OpenText(ofd.FileName))
                    {
                        using (var reader = new CsvReader(streamReader, new CultureInfo(1)))
                        {
                            reader.Read();
                            reader.ReadHeader();
                            if (dbService is PolicyService)
                            {
                                reader.Context.RegisterClassMap<PM>();
                                Policy objectToAdd = reader.GetRecord<Policy>();
                                ((PolicyService)dbService).Add(objectToAdd);
                            }
                            else if (dbService is EmployeeService)
                            {
                                reader.Context.RegisterClassMap<EM>();
                                Employee objectToAdd = reader.GetRecord<Employee>();
                                ((EmployeeService)dbService).Add(objectToAdd);
                            }
                            else if (dbService is CaseService)
                            {
                                reader.Context.RegisterClassMap<CM>();
                                Case objectToAdd = reader.GetRecord<Case>();
                                ((CaseService)dbService).Add(objectToAdd);
                            }
                            this.CurrentFile = new FileInfo(ofd.FileName);
                        }
                    }
                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show("Nie podano ścieżki (ArgumentNullException). " + ex.StackTrace, "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Błąd: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Nie odnaleziono pliku!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("Nie odnaleziono katalogu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (IOException)
                {
                    MessageBox.Show("Błąd otwarcia pliku!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        /// <summary>
        /// Metoda Save klasy.
        /// </summary>
        /// <param name="objectToSave">Obiekt, który ma być zapisany w pliku .csv.</param>
        /// <param name="fileName">Ścieżka do pliku.</param>
        /// <remarks>
        /// Zapisuje aktualnie wybrany obiekt w aktualnie otwartym pliku .csv.
        /// </remarks>
        public void Save(T objectToSave)
        {
            if (this.CurrentFile != null)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(CurrentFile.FullName))
                    {
                        using (var writer = new CsvWriter(sw, new CultureInfo(1)))
                        {
                            object type = objectToSave;
                            if (type is Policy)
                            {
                                writer.Context.RegisterClassMap<PM>();
                            }
                            else if (type is Case)
                            {
                                writer.Context.RegisterClassMap<CM>();
                            }
                            else if (type is Employee)
                            {
                                writer.Context.RegisterClassMap<EM>();
                            }
                            writer.WriteRecord(objectToSave);
                        }
                        sw.Close();
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("Brak uprawnień do podanego katalogu! " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Nie podano ścieżki (ArgumentNullException).", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Błąd: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("Nie odnaleziono katalogu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (PathTooLongException)
                {
                    MessageBox.Show("Podana ścieżka jest zbyt długa!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (IOException)
                {
                    MessageBox.Show("Błąd zapisu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show("Błąd: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        /// <summary>
        /// Metoda SaveAs klasy.
        /// </summary>
        /// <param name="objectToSave">Obiekt, kóry ma być zapisany w pliku.</param>
        /// <remarks>
        /// Otwiera okno zapisu pliku, po czym wywyłuje metodę Save tej klasy.
        /// </remarks>
        public void SaveAs(T objectToSave)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Plik *.csv|*.csv;";
            if (sfd.ShowDialog() == true)
            {
               this.CurrentFile = new FileInfo(sfd.FileName);
                Save(objectToSave);
            }
        }
    }
}
