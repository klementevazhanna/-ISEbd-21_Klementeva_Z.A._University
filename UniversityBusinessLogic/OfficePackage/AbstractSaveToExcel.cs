using UniversityBusinessLogic.OfficePackage.HelperEnums;
using UniversityBusinessLogic.OfficePackage.HelperModels;

namespace UniversityBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        public void CreateReport(WordExcelInfo info)
        {
            CreateExcel(info);

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            uint rowIndex = 2;
            foreach (var education in info.Educations)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = education.Name,
                    StyleInfo = ExcelStyleInfoType.Text
                });

                rowIndex++;
                foreach (var discipline in education.Disciplines)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = discipline.Name,
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = discipline.Price.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });

                    rowIndex++;
                }

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "C",
                    RowIndex = rowIndex,
                    Text = education.Cost.ToString(),
                    StyleInfo = ExcelStyleInfoType.Text
                });

                rowIndex++;
            }

            SaveExcel(info);
        }

        protected abstract void CreateExcel(WordExcelInfo info);

        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

        protected abstract void MergeCells(ExcelMergeParameters excelParams);

        protected abstract void SaveExcel(WordExcelInfo info);
    }
}
