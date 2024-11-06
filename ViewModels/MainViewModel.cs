using RoomNumberTA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RoomNumberTA.Interfaces;
using RoomNumberTA.Core;
using System.Windows;
using RoomNumberTA.Commands;
using System.Windows.Navigation;
namespace RoomNumberTA.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private IEnumerable<IParameter> _parameters;
        private IParameter _selectedParameter;
        private IElementsSelector _elementsSelector;
        private IElementSelector _elemSelector;
        private IEnumerable<IElem> _elems;
        private IElem _sourceElem;
        private int _elementsCount;
        private bool _isParamsEnabled;
        private bool _isSourceElemEnabled;
        private bool _isProcessEnabled;
        public MainViewModel(List<IParameter> parameters, IElementsSelector elementsSelector, IElementSelector elementSelector)
        {
            _parameters = parameters;
            _elementsSelector = elementsSelector;
            _elemSelector = elementSelector;
            IsParamsEnabled=IsProcessEnabled=IsSourceElemEnabled = false;
            SelectElems();
        }
        
        public IEnumerable<IParameter> Parameters
        {
            get=> _parameters;
        }
        
        public IParameter SelectedParameter
        {
            get =>_selectedParameter;
            set
            {
                _selectedParameter = value;
                CheckEnabled();
                OnPropertyChanged(nameof(SelectedParameter));
            }
        }
        public int ElementsCount
        {
            get => _elementsCount;
            set
            {
                _elementsCount = value;
                OnPropertyChanged(nameof(ElementsCount));
            }
        }
        public bool IsParamsEnabled
        {
            get=> _isParamsEnabled;
            set
            {
                _isParamsEnabled = value;
                OnPropertyChanged(nameof(IsParamsEnabled));
            }
        }

        public bool IsSourceElemEnabled
        {
            get => _isSourceElemEnabled;
            set
            {
                _isSourceElemEnabled = value;
                OnPropertyChanged(nameof(IsSourceElemEnabled));
            }
        }
        public bool IsProcessEnabled
        {
            get => _isProcessEnabled;
            set
            {
                _isProcessEnabled = value;
                OnPropertyChanged(nameof(IsProcessEnabled));
            }
        }
        public string SourceElemName
        {
            get => _sourceElem == null?string.Empty:_sourceElem.Name;
            set
            {
                OnPropertyChanged(nameof(SourceElemName));
            }
        }
        public IElem SourceElem
        {
            get => _sourceElem;
            set
            {
                _sourceElem = value;
                OnPropertyChanged(nameof(SourceElemName));
                OnPropertyChanged(nameof(SourceElem));
            }
        }

        public RelayCommand SelectElementsCommand => new RelayCommand(o =>
        {
            SelectElems();
        });
        public RelayCommand SelectSourceCommand => new RelayCommand(o =>
        {
            SelectSourceElem();
        });
        public RelayCommand StartProcessCommand => new RelayCommand(o =>
        {
            Cmd.Handler.TransactAction = () =>
            {
                using (Transaction tr = new Transaction(Data.Doc,"CopyParamTr"))
                {
                    tr.Start();
                    CopyParameter();
                    tr.Commit();
                }

            };
            Cmd.ExEvent.Raise();
        });

        public void SelectElems()
        {
            _elems=_elementsSelector.SelectElements(Data.Doc, Data.UiDocument);
            ElementsCount = _elems.Count();
            CheckEnabled();
        }
        public void SelectSourceElem()
        {
            SourceElem = _elemSelector.SelectElement(Data.Doc, Data.UiDocument);
            CheckEnabled();
        }

        public void CopyParameter()
        {
            int resultCount = SelectedParameter.Copy(Data.Doc,SourceElem.Id, _elems.Select(o => o.Id));
            if(resultCount==-1)
            {
                MessageBox.Show("Операция не выполнена!\nПроверьте наличие параметра у выбранного в качестве источника элемента.");
                return;
            }
            MessageBox.Show($"Параметер {SelectedParameter.Name} успешно записано в {resultCount} элементов");
        }

        public void CheckEnabled()
        {
            if (ElementsCount > 0 && SelectedParameter!=null && SourceElem!=null)
            {
                IsSourceElemEnabled = IsParamsEnabled = IsProcessEnabled = true;
            }
            else if (ElementsCount > 0 && SelectedParameter != null)
            {
                IsSourceElemEnabled = IsParamsEnabled = true;
                IsProcessEnabled = false;
            }
            else if (ElementsCount > 0)
            {
                IsParamsEnabled = true;
                IsSourceElemEnabled = IsProcessEnabled = false;
            }
            else
            {
                IsSourceElemEnabled = IsParamsEnabled = IsProcessEnabled = false;
            }
        }
    }
}
