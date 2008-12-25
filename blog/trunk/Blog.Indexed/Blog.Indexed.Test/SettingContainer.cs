// Copyright: Tobias Hertkorn
// Blog: http://saftsack.fs.uni-bayreuth.de/~dun3/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Indexed
{
    public class SettingContainer
    {
        public SettingContainer()
        {
            SettingByName = new IndexedProperty<string, Setting>(GetSettingByName, SetSettingByName);
        }

        private Setting GetSettingByName(string name)
        {
            return m_settingz.Find(
                (s) =>
                {
                    return s.Name == name;
                }
                );
        }

        private void SetSettingByName(string name, Setting setting)
        {
            var index = m_settingz.FindIndex(
                 (u) =>
                 {
                     return u.Name == name;
                 }
             );
            if (index < 0)
            {
                // setting  was not found -> append to list
                m_settingz.Add(setting);
            }
            else
            {
                m_settingz[index] = setting;
            }
        }

        private List<Setting> m_settingz = new List<Setting>();
        public IList<Setting> Settingz
        {
            get
            {
                return m_settingz;
            }
        }

        public IndexedProperty<string, Setting> SettingByName { get; private set; }

        public class Setting
        {
            public string Name { get; set; }
        }
    }
}
