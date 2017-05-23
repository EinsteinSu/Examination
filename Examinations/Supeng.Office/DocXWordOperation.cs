using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Novacode;

namespace Supeng.Office
{
    public class DocXWordOperation : IWordOperation
    {
        private readonly DocX _doc;

        public DocXWordOperation(string fileName)
        {
            _doc = !File.Exists(fileName) ? DocX.Create(fileName) : DocX.Load(fileName);
        }

        public void Dispose()
        {
            _doc.Save();
            _doc.Dispose();
        }

        public DocX Doc
        {
            get { return _doc; }
        }

        public void InsertText(string bookMark, string text)
        {
            var item = _doc.Bookmarks[bookMark];
            if (item != null)
            {
                item.Paragraph.InsertText(text);
            }
        }

        public int FindAndReplace(string find,string replaceText)
        {
            var item = _doc.FindAll(find);
            foreach (var list in _doc.Lists)
            {
                foreach (var paragraph in list.Items)
                {
                    if (paragraph.IndentLevel == 1)
                    {
                        _doc.RemoveParagraph(paragraph);
                    }
                }
            }
            return 0;
        }

        public void InsertTable(string bookMark, ITableInsert tableInsert,
            TableDesign tableDesign = TableDesign.TableGrid)
        {
            var item = _doc.Bookmarks[bookMark];
            if (item != null)
            {
                var table = _doc.AddTable(tableInsert.RowCount, tableInsert.ColumnCount);
                table.Design = tableDesign;
                tableInsert.InsertHead(table);
                tableInsert.FillData(table);
                item.Paragraph.InsertTableAfterSelf(table);
            }
        }

        public void Replace(IList<WordReplaceInfo> replaces)
        {
            foreach (var item in replaces)
            {
                if (string.IsNullOrEmpty(item.Newstring))
                {
                    item.Newstring = " ";
                }
                _doc.ReplaceText(item.Oldsring, item.Newstring);
            }
        }
    }

    public static class DocXTableExtensions
    {
        public static void SetColumn(this Table table, int cellIndex, int width, string text, int row = 0)
        {
            if (width != 0)
            {
                table.Rows[row].Cells[cellIndex].Width = width;
            }
            table.Rows[row].Cells[cellIndex].Paragraphs[0].Append(text);
        }
    }
}