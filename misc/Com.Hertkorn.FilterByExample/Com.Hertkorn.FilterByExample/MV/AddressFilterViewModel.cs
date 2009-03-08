using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.FilterByExample.M;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Com.Hertkorn.Framework.FilterByExample;
using System.Linq.Expressions;
using System.Windows;

namespace Com.Hertkorn.FilterByExample.MV
{
    public class AddressFilterViewModel : INotifyPropertyChanged
    {
        public AddressFilterViewModel()
        {
            m_addressz = (new AddressRepository()).GetAll();
            m_filter = new Address();
            m_exclude = new Expression<Func<Address, object>>[] 
            {
                c => c.City,
                c => c.Country,
                c => c.Street,
                c => c.StreetNumber,
                c => c.Zip
            };
        }

        private List<Address> m_addressz;

        public List<Address> Addressz
        {
            get
            {
                return m_addressz.FilterByExample(m_filter, m_exclude).ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            PropertyChangedEventHandler temp = PropertyChanged;

            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(info));
            }
        }

        private Address m_filter;
        private Expression<Func<Address, object>>[] m_exclude;

        internal void SetFilter(string street, string streetNumber, string zip, string city, object country)
        {
            List<Expression<Func<Address, object>>> exclude = new List<Expression<Func<Address, object>>>();
            Address address = new Address();

            if (!string.IsNullOrEmpty(city))
            {
                address.City = city;
            }
            else
            {
                exclude.Add(x => x.City);
            }

            if (country is Country)
            {
                address.Country = (Country)country;
            }
            else
            {
                exclude.Add(x => x.Country);
            }

            if (!string.IsNullOrEmpty(street))
            {
                address.Street = street;
            }
            else
            {
                exclude.Add(x => x.Street);
            }

            if (!string.IsNullOrEmpty(streetNumber))
            {
                address.StreetNumber = streetNumber;
            }
            else
            {
                exclude.Add(x => x.StreetNumber);
            }

            int zipAsInt;
            if (!string.IsNullOrEmpty(zip) && int.TryParse(zip, out zipAsInt))
            {
                address.Zip = zipAsInt;
            }
            else
            {
                exclude.Add(x => x.Zip);
            }

            m_filter = address;
            m_exclude = exclude.ToArray();

            NotifyPropertyChanged("Addressz");
        }
    }
}
