using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;


namespace TestApp
{
    public class PhoneRepository
    {
        private PhoneContext db;
        public ObservableCollection<Phone> localPhones { get; private set; }

        public PhoneRepository(PhoneContext context)
        {
            db = context;
            db.Phones.Load();
            localPhones = db.Phones.Local;
        }

        public IEnumerable<Phone> GetAll()
        {
            return db.Phones;
        }

        public IEnumerable<Phone> GetPhonesByName(string name, string surname)
        {
            return db.Phones.Where(p => p.Name == name && p.Surname == surname);
        }

        public Phone Get(int id)
        {
            return db.Phones.Find(id);
        }

        public void Create(Phone phone)
        {
            db.Phones.Add(phone);
        }

        public void Update(Phone phone)
        {
            db.Entry(phone).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Phone book = db.Phones.Find(id);
            if (book != null)
                db.Phones.Remove(book);
        }
    }
}
