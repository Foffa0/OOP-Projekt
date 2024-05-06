using System.Collections;

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


        /// <summary>
        /// Removes a given Element from List
        /// </summary>
        /// /// <param name="data">The data to remove</param>
        public void Remove(T data)  // Returns the next list element
        {
            if (data == null)
            {
                throw new ArgumentNullException("Data cannot be null");
            }

            if (head != null && data.Equals(head.data))
            {
                head = head.next;
            }

            Element? previous = FindPrevious(data);

            if (previous == null)
            {
                return;     // OR: throw new ArgumentNullException("Element not found in list!");
            }
            else
            {
                previous.next = previous.next!.next; // previous.next cannot be null, since this is the element to delete
            }
        }

        public Element? FindPrevious(T data)
        {
            if (data == null)
                throw new ArgumentNullException("Previous cannot be found when data is null");

            Element? previous = head;

            while (previous != null)
            {
                if (previous.next != null && data.Equals(previous.next.data))
                {
                    return previous;
                }

                previous = previous.next;
            }

            return null;    // Not found in list
        }

        public void Clear()
        {
            head = null;
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
