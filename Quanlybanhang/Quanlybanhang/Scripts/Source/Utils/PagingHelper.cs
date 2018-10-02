using Quanlybanhang.Scripts.Source.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Quanlybanhang.Scripts.Source.Utils
{
    public class PagingHelper
    {
        protected int _currentPage = 1;
        protected int _pageSize = 10;
        protected int _totalPage = 0;
        protected PlaceHolder _placeHolder;
        protected IDataComponent _dataComponent;
        protected Repeater _repeater;
        
        public PagingHelper(Control controllPage, PlaceHolder placeholder, IDataComponent data, Repeater rpt)
        {
            _placeHolder = placeholder;
            _dataComponent = data;
            _repeater = rpt;
            //if(controllPage.ViewStateMode)
        }

        public void FetchData()
        {
            
            _repeater.DataSource = _dataComponent.GetDataByPage(_pageSize, _currentPage);
            _repeater.DataBind();
        }

        public void CreatePagingControl()
        {
            _placeHolder.Controls.Clear();
            _totalPage = _dataComponent.GetTotalPage(_pageSize);
            string script = "<nav><ul class='pagination'>";
            _placeHolder.Controls.Add(new LiteralControl(script));
            if (_currentPage > 1)
            {
                script = "<li class='page-item'>";
                _placeHolder.Controls.Add(new LiteralControl(script));
                LinkButton lnk = new LinkButton();
                lnk.Click += new EventHandler(lbl_Clickback);
                lnk.ID = "back";
                lnk.Text = "<<";
                _placeHolder.Controls.Add(lnk);
                script = "</li>";
                _placeHolder.Controls.Add(new LiteralControl(script));

            }
            for (int i = 1; i < _totalPage + 1; i++)
            {

                if (i == _currentPage)
                {
                    script = "<li class='disable'>";
                    _placeHolder.Controls.Add(new LiteralControl(script));
                    LinkButton lnk = new LinkButton();
                    lnk.ID = "lnkPage" + (i).ToString();
                    lnk.Text = (i).ToString();
                    lnk.Enabled = false;
                    _placeHolder.Controls.Add(lnk);
                    script = "</li>";
                    _placeHolder.Controls.Add(new LiteralControl(script));
                }
                else
                {
                    script = "<li class='page-item'>";
                    _placeHolder.Controls.Add(new LiteralControl(script));
                    LinkButton lnk = new LinkButton();
                    lnk.Click += new EventHandler(lbl_Click);
                    lnk.ID = "lnkPage" + (i).ToString();
                    lnk.Text = (i).ToString();
                    _placeHolder.Controls.Add(lnk);
                    script = "</li>";
                    _placeHolder.Controls.Add(new LiteralControl(script));
                }
                Label spacer = new Label();
                spacer.Text = "&nbsp;";
                _placeHolder.Controls.Add(spacer);

            }
            if (_currentPage < _totalPage)
            {
                script = "<li class='page-item'>";
                _placeHolder.Controls.Add(new LiteralControl(script));
                LinkButton lnk = new LinkButton();
                lnk.Click += new EventHandler(lbl_Clicknext);
                lnk.ID = "next";
                lnk.Text = ">>";
                _placeHolder.Controls.Add(lnk);
                script = "</li>";
                _placeHolder.Controls.Add(new LiteralControl(script));

            }
            script = "</ul></nav>";
            _placeHolder.Controls.Add(new LiteralControl(script));
        }

        void lbl_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            _currentPage = int.Parse(lnk.Text);

            FetchData();
            CreatePagingControl();
        }

        void lbl_Clicknext(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            _currentPage = _currentPage + 1;

            FetchData();
            CreatePagingControl();
        }

        void lbl_Clickback(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            _currentPage = _currentPage - 1;

            FetchData();
            CreatePagingControl();
        }
    }
}