using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using WWWGame.DataAccessLibrary;
using WWWGame.LogicLayer.Repositories;

namespace WWWGame.UI.ViewModel
{
    public class ImageViewModel : ViewModelBase
    {
        private readonly ITournsRepository _repository = new TournsRepository();
        private byte[] _data;
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                Data = _repository.GetImage(value);
            }
        }

        public byte[] Data
        {
            get { return _data; }
            set { Set("Data", ref _data, value); }
        }
    }

}
