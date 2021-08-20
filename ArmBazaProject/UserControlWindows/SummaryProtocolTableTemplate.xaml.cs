using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace ArmBazaProject.UserControlWindows
{
    /// <summary>
    /// Логика взаимодействия для SummaryProtocolTableTemplate.xaml
    /// </summary>
    public partial class SummaryProtocolTableTemplate : UserControl
    {
        public SummaryProtocolTableTemplate()
        {
            InitializeComponent();
        }

        private void svT_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
            {
                scrollviewer.LineUp();
            }
            else
            {
                scrollviewer.LineDown();
            }
        }

        private void exportToExcel_Click(object sender, RoutedEventArgs e)
        {
            /*Excel.Application excel = new Excel.Application();
            excel.Visible = true; 
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < dataGridResult.Columns.Count; j++) 
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; 
                sheet1.Columns[j + 1].ColumnWidth = 15; 
                myRange.Value2 = dataGridResult.Columns[j].Header;
            }
            for (int i = 0; i < dataGridResult.Columns.Count; i++)
            { //www.yazilimkodlama.com
                for (int j = 0; j < dgrid.Items.Count; j++)
                {
                    TextBlock b = dgrid.Columns[i].GetCellContent(dgrid.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }*/
        }

        

    }
}
