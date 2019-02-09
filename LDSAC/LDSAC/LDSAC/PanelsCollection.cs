using System;
using System.Collections.Generic;
using System.Text;
using OpenSystems.Windows.Controls;
using System.Windows.Forms;

namespace LDSAC
{
    public class PanelsCollection : System.Collections.CollectionBase
    {
        /// <summary>
        /// Asigna u obtiene un objeto de la colección
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Control this[int index]
        {
            get { return ((Control)List[index]); }
            set { List[index] = value; }
        }

        /// <summary>
        /// Adiciona el objeto a la colección
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(Control value)
        {
            return (List.Add(value));
        }

        /// <summary>
        /// Retorna el indice del objeto en la colecció n
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(Control value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>
        /// Inserta el objeto en la colección
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, Control value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// Remueve el objeto de la colección
        /// </summary>
        /// <param name="value"></param>
        public void Remove(Control value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Si el valor no es del tipo OpenSteepPanel, retorna falso
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(Control value)
        {
            return (List.Contains(value));
        }

        /// <summary>
        /// Código adicional cuando inserte objetos
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        protected override void OnInsert(int index, Object value)
        {
        }

        /// <summary>
        /// Código adicional cuando remueve objetos
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        protected override void OnRemove(int index, Object value)
        {
        }

        /// <summary>
        /// Código adicional cuando setee objetos
        /// </summary>
        /// <param name="index"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
        }

        /// <summary>
        /// Código para validar que el tipo de objeto sea el adecuado
        /// </summary>
        /// <param name="value"></param>
        protected override void OnValidate(Object value)
        {
        }   
    }
}
