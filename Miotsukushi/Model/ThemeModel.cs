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
        public Accent[] accents;
        public AppTheme[] themes;

        public string[] accent_name;
        public string[] theme_name;

        private int _selected_theme;

        public int selected_theme
        {
            get
            {
                return _selected_theme;
            }

            set
            {
                if (_selected_theme != value && 0 <= value && value < themes.Length)
                {
                    _selected_theme = value;
                    ThemeManager.ChangeAppStyle(App.Current, accents[_selected_accent], themes[value]);
                    Properties.Settings.Default.Theme = theme_name[value];
                    Properties.Settings.Default.Save();
                }
            }
        }

        private int _selected_accent;

        public int selected_accent
        {
            get { return _selected_accent; }
            set
            {
                if(_selected_accent != value && 0 <= value && value < accents.Length)
                {
                    _selected_accent = value;
                    ThemeManager.ChangeAppStyle(App.Current, accents[value], themes[_selected_theme]);
                    Properties.Settings.Default.AccentColor = accent_name[value];
                    Properties.Settings.Default.Save();
                }
            }
        }

        public ThemeModel()
        {
            accents = ThemeManager.Accents.ToArray();
            themes = ThemeManager.AppThemes.ToArray();

            accent_name = new string[accents.Length];
            for (var i = 0; i < accents.Length; i++)
                accent_name[i] = accents[i].Name;

            theme_name = new string[themes.Length];
            for (var i = 0; i < themes.Length; i++)
                theme_name[i] = themes[i].Name;

            if (GetThemeIndex(Properties.Settings.Default.Theme) != -1 && GetAccentIndex(Properties.Settings.Default.AccentColor) != -1)
            {
                _selected_theme = GetThemeIndex(Properties.Settings.Default.Theme);
                _selected_accent = GetAccentIndex(Properties.Settings.Default.AccentColor);
                ThemeManager.ChangeAppStyle(App.Current, accents[_selected_accent], themes[_selected_theme]);
            }
        }

        public void ChangeStyle(string theme, string accent)
        {
            var acc_index = GetAccentIndex(accent);
            var thm_index = GetThemeIndex(theme);

            if (acc_index == -1)
                throw new ArgumentException("accent");
            if (thm_index == -1)
                throw new ArgumentException("theme");

            ThemeManager.ChangeAppStyle(App.Current, accents[acc_index], themes[thm_index]);
        }


        public int GetAccentIndex(string Accent)
        {
            for (var i = 0; i < accent_name.Length; i++)
                if (Accent == accent_name[i])
                    return i;
            return -1;
        }

        public int GetThemeIndex(string Theme)
        {
            for (var i = 0; i < theme_name.Length; i++)
                if (Theme == theme_name[i])
                    return i;
            return -1;
        }
    }
}
