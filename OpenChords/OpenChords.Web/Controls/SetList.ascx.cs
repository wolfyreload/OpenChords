using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web.Controls
{
    public partial class SetList : System.Web.UI.UserControl
    {
        protected SetList _LinkToSelf;

        public event EventHandler SelectedSetChanged; 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public override void DataBind()
        {
            lstViewSets.DataBind();
        }

        protected void txtSearchSet_TextChanged(object sender, EventArgs e)
        {
            lstViewSets.DataBind();
        }

        public string SelectedSet
        {
            get
            {
                return (string)lstViewSets.SelectedValue;
            }
            set
            {
                for (int i = 0; i < lstViewSets.Items.Count; i++)
                {
                    var item = (LinkButton)lstViewSets.Items[i].FindControl("lnkSets");
                    if (item.Text == value)
                    {
                        lstViewSets.SelectedIndex = i;
                        lstViewSets.DataBind();
                        break;
                    }
                    
                }
                
            }
        }
        

        class RandomMappingClass
        {
            public string Name { get; set; }
        }

        protected void lstViewSets_DataBinding(object sender, EventArgs e)
        {
            var allSets = OpenChords.Entities.Set.listOfAllSets();

            var filter = txtSearchSet.Text.ToUpper();
            if (!string.IsNullOrEmpty(filter))
            {
                allSets = allSets.Where(a => a.ToUpper().Contains(filter)).ToList();
            }
            var filteredSets = allSets.Select(s => new RandomMappingClass() { Name = s });
            lstViewSets.DataSource = filteredSets;

        }

        protected void lstViewSets_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            lstViewSets.SelectedIndex = e.NewSelectedIndex;
            lstViewSets.DataBind();
        }

        protected void lstViewSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedSetChanged != null)
                SelectedSetChanged(this, e);
        }
            

    }
}