using DevExpress.Data.Linq.Helpers;
using DevExpress.Web.Mvc;
using Supeng.Examination.BusinessLayer.Interfaces;

namespace Management.Commons
{
    public interface IDxPagingWapper
    {
        GridViewModel GetViewModel(string gridName = "gridView");
    }

    public class DxPagingWapper : IDxPagingWapper
    {
        private readonly string[] _items;
        private readonly string _keyItem;
        private readonly IPaging _paging;

        public DxPagingWapper(IPaging paging, string keyItem, params string[] items)
        {
            _paging = paging;
            _keyItem = keyItem;
            _items = items;
        }

        public GridViewModel GetViewModel(string gridName = "gridView")
        {
            var viewModel = GridViewExtension.GetViewModel(gridName) ?? CreateViewModel();
            viewModel.ProcessCustomBinding(
                GetDataRowCount,
                GetData
                );
            return viewModel;
        }

        protected GridViewModel CreateViewModel()
        {
            var viewModel = new GridViewModel {KeyFieldName = _keyItem};
            foreach (var item in _items)
            {
                viewModel.Columns.Add(item);
            }

            //todo: load it from configurations or database
            viewModel.Pager.PageSize = 20;

            return viewModel;
        }

        protected void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {
            e.DataRowCount = _paging.Model.Count();
        }

        protected void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _paging.Model.ApplySorting(e.State.SortedColumns).Skip(e.StartDataRowIndex).Take(e.DataRowCount);
        }
    }
}