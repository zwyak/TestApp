using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;


namespace TestApp
{
    public class PhoneViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Phone> Phones { get; set; }
        public ObservableCollection<Phone> PhonesSearched { get; set; }
        public Phone selectedPhone;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        UnitOfWork db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        RelayCommand searchCommand;

        public PhoneViewModel()
        {
            db = new UnitOfWork();
            Phones = db.Phones.localPhones;
        }

         public Phone SelectedPhone
         {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
         }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      PhoneWindow phoneWindow = new PhoneWindow(new Phone());
                      if (phoneWindow.ShowDialog() == true)
                      {
                          Phone phone = phoneWindow.Phone;
                          db.Phones.Create(phone);
                          db.Save();
                      }
                  }));
            }
        }

        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ??
                  (searchCommand = new RelayCommand((o) =>
                  {
                      Message = string.Empty;
                      OnPropertyChanged("Message");

                      if ((Name == null || Name.Length == 0) && (Surname == null || Surname.Length == 0))
                      {
                          Message = "Input search parameters";
                          OnPropertyChanged("Message");
                          return;
                      }
                          
                      Phone phone = null;
                      phone = Phones.FirstOrDefault(p => p.Name == Name && p.Surname == Surname);
                      if (phone == null) phone = Phones.FirstOrDefault(p => p.Name.StartsWith(Name) && p.Surname.StartsWith(Surname));
                      SelectedPhone = phone;
                  }));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((o) =>
                  {
                      if (selectedPhone == null) return;
                      Phone phone = (Phone)o;
                      PhoneWindow phoneWindow = new PhoneWindow(phone);

                      if (phoneWindow.ShowDialog() == true)
                      {
                          db.Phones.Update(phone);
                          db.Save();
                      }

                  }));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((o) =>
                  {
                      if (o == null) return;
                      Phone phone = (Phone)o;
                      db.Phones.Delete(phone.Id);
                      db.Save();
                  }));
            }
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
