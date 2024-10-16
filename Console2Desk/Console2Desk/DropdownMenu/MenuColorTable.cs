using System.Windows.Forms;
using System.Drawing;

namespace Console2Desk.DropdownMenu
{
    public class MenuColorTable : ProfessionalColorTable
    {
        //Fields
        private Color backColor;
        private Color leftColumnColor;
        private Color borderColor;
        private Color menuItemBorderColor;
        private Color menuItemSelectedColor;

        //Constructor
        public MenuColorTable(bool isMainMenu, Color primaryColor)
        {
            if (isMainMenu)
            {
                backColor = Color.FromArgb(40, 40, 40);
                leftColumnColor = Color.FromArgb(31, 31, 31);
                borderColor = Color.FromArgb(31, 31, 31);
                menuItemBorderColor = primaryColor;
                menuItemSelectedColor = primaryColor;
            }
            else
            {
                backColor = Color.FromArgb(40, 40, 40);
                leftColumnColor = Color.FromArgb(31, 31, 31);
                borderColor = Color.FromArgb(31, 31, 31);
                menuItemBorderColor = primaryColor;
                menuItemSelectedColor = primaryColor;
            }
        }

        //Overrides
        public override Color ToolStripDropDownBackground { get { return backColor; } }
        public override Color MenuBorder { get { return borderColor; } }
        public override Color MenuItemBorder { get { return menuItemBorderColor; } }
        public override Color MenuItemSelected { get { return menuItemSelectedColor; } }
        public override Color MenuItemSelectedGradientBegin { get { return menuItemSelectedColor; } }
        public override Color MenuItemSelectedGradientEnd { get { return menuItemSelectedColor; } }
        public override Color MenuItemPressedGradientBegin { get { return menuItemSelectedColor; } }
        public override Color MenuItemPressedGradientMiddle { get { return menuItemSelectedColor; } }
        public override Color MenuItemPressedGradientEnd { get { return menuItemSelectedColor; } }
        public override Color ImageMarginGradientBegin { get { return leftColumnColor; } }
        public override Color ImageMarginGradientMiddle { get { return leftColumnColor; } }
        public override Color ImageMarginGradientEnd { get { return leftColumnColor; } }

        // Aggiungi questi override per gestire completamente i colori di hover
        public override Color ButtonSelectedBorder { get { return menuItemSelectedColor; } }
        public override Color ButtonSelectedGradientBegin { get { return menuItemSelectedColor; } }
        public override Color ButtonSelectedGradientEnd { get { return menuItemSelectedColor; } }
        public override Color ButtonSelectedGradientMiddle { get { return menuItemSelectedColor; } }
        public override Color ButtonSelectedHighlight { get { return menuItemSelectedColor; } }
        public override Color ButtonSelectedHighlightBorder { get { return menuItemSelectedColor; } }
    }
}
