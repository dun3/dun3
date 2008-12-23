using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Indexed
{
    public class ReadonlySettingContainer
    {
        public ReadonlySettingContainer()
        {
            SettingByName = new IndexedGetProperty<string, Setting>(
                (name) =>
                {
                    return m_settingz.Find(
                        (s) =>
                        {
                            return s.Name == name;
                        }
                        );
                }
                );
        }

        private List<Setting> m_settingz = new List<Setting>();
        public IList<Setting> Settingz
        {
            get
            {
                return m_settingz;
            }
        }

        public IndexedGetProperty<string, Setting> SettingByName { get; private set; }

        public class Setting
        {
            public string Name { get; set; }
        }
    }
}
