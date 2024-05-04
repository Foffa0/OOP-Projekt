using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Models
{
    internal class LinkedList<T> : IEnumerable<T>
    {
        public class Element
        {
            public Element? next;
            public T data;

            public Element(T data)
            {
                this.data = data;
            }

            public Element(T data, Element next) : this(data)
            {
                this.next = next;
            }
        }

        public Element? head {  get; set; }

        /// <summary>
        /// Adds new ListElement in front of List
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Element Add(T data)
        {
            if (head == null)
            {
                head = new Element(data);
                return head;
            }

            Element current = head;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = new Element(data);
            return current.next;
        }

        /// <summary>
        /// Removes first Element from List and returns its data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T Remove()
        {
            if (head == null)
                throw new Exception("Cannot remove from empty list");

            T data = head.data;
            head = head.next;
            return data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Element? current = head;
            while (current != null)
            {
                yield return current.data;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
