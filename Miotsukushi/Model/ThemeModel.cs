using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro;

namespace Miotsukushi.Model
{
    class ThemeModel
    {
        public Accent[] Accents;
        public AppTheme[] Themes;

        public string[] AccentName;
        public string[] ThemeName;

        private int _selectedTheme;

        public int SelectedTheme
        {
            get
            {
                return _selectedTheme;
            }

            set
            {
                if (_selectedTheme != value && 0 <= value && value < Themes.Length)
                {
                    _selectedTheme = value;
                    ThemeManager.ChangeAppStyle(App.Current, Accents[_selectedAccent], Themes[value]);
                    Properties.Settings.Default.Theme = ThemeName[value];
                    Properties.Settings.Default.Save();
                }
            }
        }

        private int _selectedAccent;

        public int SelectedAccent
        {
            get { return _selectedAccent; }
            set
            {
                if(_selectedAccent != value && 0 <= value && value < Accents.Length)
                {
                    _selectedAccent = value;
                    ThemeManager.ChangeAppStyle(App.Current, Accents[value], Themes[_selectedTheme]);
                    Properties.Settings.Default.AccentColor = AccentName[value];
                    Properties.Settings.Default.Save();
                }
            }
        }

        public ThemeModel()
        {
            Accents = ThemeManager.Accents.ToArray();
            Themes = ThemeManager.AppThemes.ToArray();

            AccentName = new string[Accents.Length];
            for (var i = 0; i < Accents.Length; i++)
                AccentName[i] = Accents[i].Name;

            ThemeName = new string[Themes.Length];
            for (var i = 0; i < Themes.Length; i++)
                ThemeName[i] = Themes[i].Name;

            if (GetThemeIndex(Properties.Settings.Default.Theme) != -1 && GetAccentIndex(Properties.Settings.Default.AccentColor) != -1)
            {
                _selectedTheme = GetThemeIndex(Properties.Settings.Default.Theme);
                _selectedAccent = GetAccentIndex(Properties.Settings.Default.AccentColor);
                ThemeManager.ChangeAppStyle(App.Current, Accents[_selectedAccent], Themes[_selectedTheme]);
            }
        }

        public void ChangeStyle(string theme, string accent)
        {
            var accIndex = GetAccentIndex(accent);
            var thmIndex = GetThemeIndex(theme);

            if (accIndex == -1)
                throw new ArgumentException("accent");
            if (thmIndex == -1)
                throw new ArgumentException("theme");

            ThemeManager.ChangeAppStyle(System.Windows.Application.Current, Accents[accIndex], Themes[thmIndex]);
        }


        public int GetAccentIndex(string accent)
        {
            for (var i = 0; i < AccentName.Length; i++)
                if (accent == AccentName[i])
                    return i;
            return -1;
        }

        public int GetThemeIndex(string theme)
        {
            for (var i = 0; i < ThemeName.Length; i++)
                if (theme == ThemeName[i])
                    return i;
            return -1;
        }
    }
}
