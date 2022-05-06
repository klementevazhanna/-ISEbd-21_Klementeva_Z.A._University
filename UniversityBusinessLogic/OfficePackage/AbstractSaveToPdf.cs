using System.Collections.Generic;
using UniversityBusinessLogic.OfficePackage.HelperEnums;
using UniversityBusinessLogic.OfficePackage.HelperModels;

namespace UniversityBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
            CreatePdf(info);

            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string>
            {
                "3cm", "2cm", "5.5cm", "2.5cm", "3cm"
            });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> {
                    "Обучение",
                    "Кол-во",
                    "Затраты",
                    "Время",
                    "Стоимость"
                },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var education in info.Educations)
            {
                string costs = "";
                foreach (var cost in education.CostItems)
                {
                    costs += $"{cost.Name}: {cost.Sum} \n";
                }
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string>
                    {
                        education.Name,
                        education.Count.ToString(),
                        costs,
                        education.EducationDate.ToString(),
                        education.Cost.ToString()
                    },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }

            SavePdf(info);
        }

        protected abstract void CreatePdf(PdfInfo info);

        protected abstract void CreateParagraph(PdfParagraph paragraph);

        protected abstract void CreateTable(List<string> columns);

        protected abstract void CreateRow(PdfRowParameters rowParameters);

        protected abstract void SavePdf(PdfInfo info);
    }
}
