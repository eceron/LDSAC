using System;
using System.Collections.Generic;
using System.Text;
using OpenSystems.Common.Interfaces;

namespace LDSAC
{
    public class DynamicCallerLDSAC : IOpenExecutable
    {
        public void Execute(Dictionary<string, object> parameters)
        {
            using (LDSAC form = new LDSAC())
            {
                form.ShowDialog();
            }
        }
    }
}