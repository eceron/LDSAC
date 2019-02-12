using System;
using System.Collections.Generic;
using System.Text;
using OpenSystems.Common.Interfaces;
using OpenSystems.Windows.Controls;

namespace LDSAC
{
    public class DynamicCallerLDSAC : IOpenExecutable
    {
        public void Execute(Dictionary<string, object> parameters)
        {

            Int64 productId = Convert.ToInt64(parameters["NodeId"].ToString());
            Object header = null;
            if (parameters.ContainsKey("Header"))
            {
                header = parameters["Header"];
            }
            using (LDSAC form = new LDSAC(productId, (header as OpenHeaderTitles)))
            {
                form.ShowDialog();
            }
        }
    }
}