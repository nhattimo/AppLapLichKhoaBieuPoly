using Guna.UI2.WinForms.Helpers;
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace AppLapLichThoiKhoaBieuPoly
{
    public static class Management
    {
        public static void ScrollBarFlowLayoutPanel(FlowLayoutPanel flowLayoutPanel, Guna2VScrollBar vScrollBar)
        {
            PanelScrollHelper panelScrollHelper = new PanelScrollHelper(flowLayoutPanel, vScrollBar);
        }
        public static void BtnColerClick(Guna2GradientTileButton btn, Color colorBtn)
        {
            btn.BackColor = colorBtn;
        }
        public static void BtnRefreshColerTransparentClick(Guna2GradientTileButton[] btnArray, Color colorBtnArray)
        {
            foreach (var item in btnArray)
            {
                item.BackColor = colorBtnArray;
            }
        }
        public static void BtnTasbalClick(Guna2GradientTileButton[] btnArray, Color colorbtnArray, Guna2GradientTileButton btn, Color colorBtn)
        {

            // Trả array btn.BackColor về màu trong suốt
            BtnRefreshColerTransparentClick(btnArray, colorbtnArray);

            // chuyển màu Backcolor
            BtnColerClick(btn, colorBtn);
        }
        public static void BtnColerClick(Guna2GradientButton btn, Color colorBtn)
        {
            btn.BackColor = colorBtn;
        }
        public static void BtnRefreshColerTransparentClick(Guna2GradientButton[] btnArray, Color colorBtnArray)
        {
            foreach (var item in btnArray)
            {
                item.BackColor = colorBtnArray;
            }
        }
        public static void BtnTasbalClick(Guna2GradientButton[] btnArray, Color colorbtnArray, Guna2GradientButton btn, Color colorBtn)
        {

            // Trả array btn.BackColor về màu trong suốt
            BtnRefreshColerTransparentClick(btnArray, colorbtnArray);

            // chuyển màu Backcolor
            BtnColerClick(btn, colorBtn);
        }

        public static void ConvertBtn(Guna2GradientButton btn, int number)
        {
            if (number == 1)
            {
                btn.FillColor = Color.Gainsboro;
                btn.FillColor2 = Color.Gainsboro;
                btn.Text = "Thêm";
            }
            else if (number == 2)
            {
                btn.FillColor = Color.Yellow;
                btn.FillColor2 = Color.Yellow;
                btn.Text = "Sửa";
            }
        }
        // UC
        public static void UCArrayVisible(UserControl[] uCArrray, UserControl uC)
        {
            uC.Visible = true;

            foreach (var item in uCArrray)
            {
                if (item != uC)
                    item.Visible = false;
            }
        }
    }
}
