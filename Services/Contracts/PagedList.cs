using System;
using System.Collections.Generic;

namespace Warehouse.Services.Contracts {
    public class PagedList<T> {
        private List<T> _data;

        public PagedList() {
            _data = new List<T>();
        }

        public int TotalPages { get; set; }
        public int TotalData { get; set; }
        public int CurrentPage { get; set; }

        public List<T> Data {
            get {
                return _data;
            } set {
                _data = value;
            }
        }

        public PagedListInfo GetInfo() {
            return new PagedListInfo() {
                TotalPages = TotalPages,
                TotalData = TotalData,
                CurrentPage = CurrentPage
            };
        }
    }

    public class PagedListInfo {
        public int TotalPages { get; set; }
        public int TotalData { get; set; }
        public int CurrentPage { get; set; }
    }
}