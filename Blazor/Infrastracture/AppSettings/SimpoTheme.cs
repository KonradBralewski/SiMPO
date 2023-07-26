using MudBlazor;

namespace Blazor.Infrastracture.AppSettings
{
    public class SimpoTheme
    {
        private static Typography DefaultTypography = new Typography()
        {
            Default = new Default()
            {
                FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                FontSize = ".875rem",
                FontWeight = 400,
                LineHeight = 1.43,
                LetterSpacing = ".01071em"
            },
            H1 = new H1()
            {
                FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                FontSize = "6rem",
                FontWeight = 300,
                LineHeight = 1.167,
                LetterSpacing = "-.01562em"
            },
            H2 = new H2()
            {
                FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                FontSize = "3.75rem",
                FontWeight = 300,
                LineHeight = 1.2,
                LetterSpacing = "-.00833em"
            },
            H3 = new H3()
            {
                FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                FontSize = "3rem",
                FontWeight = 400,
                LineHeight = 1.167,
                LetterSpacing = "0"
            },
            H4 = new H4()
            {
                FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                FontSize = "2.125rem",
                FontWeight = 400,
                LineHeight = 1.235,
                LetterSpacing = ".00735em"
            },
            H5 = new H5()
            {
                FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                FontSize = "1.5rem",
                FontWeight = 400,
                LineHeight = 1.334,
                LetterSpacing = "0"
            },
            H6 = new H6()
            {
                FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                FontSize = "1.25rem",
                FontWeight = 400,
                LineHeight = 1.6,
                LetterSpacing = ".0075em"
            },
            Button = new Button()
            {
                FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" },
                FontSize = ".875rem",
                FontWeight = 500,
                LineHeight = 1.75,
                LetterSpacing = ".02857em"
            }
        };

        private static PaletteLight DefaultPaletteLight = new PaletteLight()
        {
            TextPrimary = Colors.Shades.Black,
            Primary = Colors.Green.Lighten2,
            SecondaryLighten = Colors.Red.Lighten2,
            Background = Colors.Indigo.Lighten4
        };

        private static PaletteDark DefaultPaletteDark = new PaletteDark()
        {
            TextPrimary = Colors.Shades.White,
            Primary = Colors.Purple.Accent3,
            Background = Colors.Grey.Darken3
        };

        public static MudTheme CustomTheme = new MudTheme()
        {
            Typography = DefaultTypography,
            Palette = DefaultPaletteLight,
            PaletteDark = DefaultPaletteDark,
        };
    }
}
