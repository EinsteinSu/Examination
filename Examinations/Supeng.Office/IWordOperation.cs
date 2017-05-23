using System;
using System.Collections.Generic;
using Novacode;

namespace Supeng.Office
{
    public interface IWordOperation : IDisposable
    {
        void InsertText(string bookMark, string text);

        void InsertTable(string bookMark, ITableInsert tableInsert, TableDesign tableDesign = TableDesign.TableGrid);

        void Replace(IList<WordReplaceInfo> replaces);
    }

    public interface ITableInsert
    {
        int RowCount { get; }

        int ColumnCount { get; }

        void InsertHead(Table table);

        void FillData(Table table);
    }
}
