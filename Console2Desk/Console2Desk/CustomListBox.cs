using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace Console2Desk
{
    [Category("Niewbie Advance")]
    public class CustomListBox : ListBox
    {
        public List<NewsItem> NewsItems { get; set; } = new List<NewsItem>();
        private string iconDirectory = @"C:\Program Files\Console2Desk\sources\";

        private Color _textColor = Color.Black;
        private Font _textFont = new Font("Arial", 12);
        private int _itemSpacing = 22;
        private bool _wordWrap = false;
        private int _iconSize = 00; // Dimensione dell'icona
        private int _textIconSpacing = 6; // Spaziatura tra testo e icona
        private int _textStatusSpacing = 15; // Spaziatura tra testo e stato
        private Color _statusColor = Color.Gray; // Colore dello stato

        [Category("Niewbie Advance")]
        [Description("Color of the text displayed in the list.")]
        public Color TextColor
        {
            get => _textColor;
            set { _textColor = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        [Description("Font used for the text displayed in the list.")]
        public Font TextFont
        {
            get => _textFont;
            set { _textFont = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        [Description("Spacing between lines of text.")]
        public int ItemSpacing
        {
            get => _itemSpacing;
            set { _itemSpacing = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        [Description("Whether to wrap text to fit the item width.")]
        public bool WordWrap
        {
            get => _wordWrap;
            set { _wordWrap = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        [Description("Size of the icons displayed in the list.")]
        public int IconSize
        {
            get => _iconSize;
            set { _iconSize = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        [Description("Spacing between the icon and text.")]
        public int TextIconSpacing
        {
            get => _textIconSpacing;
            set { _textIconSpacing = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        [Description("Spacing between the text and status.")]
        public int TextStatusSpacing
        {
            get => _textStatusSpacing;
            set { _textStatusSpacing = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        [Description("Color of the status text displayed in the list.")]
        public Color StatusColor
        {
            get => _statusColor;
            set { _statusColor = value; Invalidate(); }
        }

        public CustomListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DrawItem += CustomListBox_DrawItem;
            this.MeasureItem += CustomListBox_MeasureItem;
        }

        private void CustomListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (NewsItems != null && NewsItems.Count > 0)
            {
                var newsItem = NewsItems[e.Index];

                // Calcola la larghezza disponibile per il testo, riducendo margini inutili
                var textWidth = Width - _iconSize - _textIconSpacing - 20; // Usa 20 per margine minimo

                // Misura l'altezza del testo in base alla larghezza ottimizzata
                var textHeight = TextRenderer.MeasureText(
                    newsItem.Title,
                    _textFont,
                    new Size(textWidth, int.MaxValue),
                    _wordWrap ? TextFormatFlags.WordBreak : TextFormatFlags.Left
                ).Height;

                // Calcola l'altezza totale
                e.ItemHeight = Math.Max(
                    _iconSize,
                    textHeight + _itemSpacing + _iconSize + _textStatusSpacing
                );
            }
        }

        private void CustomListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || NewsItems == null || NewsItems.Count == 0 || e.Index >= NewsItems.Count)
            {
                return;
            }

            e.DrawBackground();

            var newsItem = NewsItems[e.Index];

            var iconPath = Path.Combine(iconDirectory, newsItem.Icon);
            Image icon;
            if (File.Exists(iconPath))
            {
                icon = Image.FromFile(iconPath);
            }
            else
            {
                icon = Image.FromFile(Path.Combine(iconDirectory, "default_icon.png"));
            }

            // Disegna l'icona, se presente
            e.Graphics.DrawImage(icon, e.Bounds.Left, e.Bounds.Top, _iconSize, _iconSize);

            // Calcola i confini per il testo, riducendo i margini laterali
            var textPadding = 10; // Riduci il padding o usa una variabile
            var textBounds = new Rectangle(
                e.Bounds.Left + _iconSize + _textIconSpacing,  // Spazio dopo l'icona
                e.Bounds.Top,
                e.Bounds.Width - _iconSize - _textIconSpacing - textPadding,  // Allarga l'area di testo
                e.Bounds.Height / 2
            );

            TextRenderer.DrawText(
                e.Graphics,
                newsItem.Title,
                _textFont,
                textBounds,
                _textColor,
                _wordWrap ? TextFormatFlags.WordBreak : TextFormatFlags.Left
            );

            // Disegna lo stato sotto il testo
            var statusPadding = 5;  // Aggiungi più spazio sotto il testo per lo stato
            var statusBounds = new Rectangle(
                e.Bounds.Left + _iconSize + _textIconSpacing,
                e.Bounds.Top + textBounds.Height + _textStatusSpacing + statusPadding,
                e.Bounds.Width - _iconSize - _textIconSpacing - textPadding,
                e.Bounds.Height / 2
            );

            TextRenderer.DrawText(
                e.Graphics,
                newsItem.Status.ToUpper(),
                _textFont,
                statusBounds,
                _statusColor,
                _wordWrap ? TextFormatFlags.WordBreak : TextFormatFlags.Left
            );

            e.DrawFocusRectangle();
        }
    }
}
