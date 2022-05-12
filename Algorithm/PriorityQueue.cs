using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> heap = new List<T>();

        public void Push(T data)
        {
            heap.Add(data);
            int _now = heap.Count - 1;

            while (_now > 0)
            {
                int _next = (_now - 1) / 2;

                if (heap[_next].CompareTo(heap[_now]) > 0)
                    break;

                T temp = heap[_next];
                heap[_next] = heap[_now];
                heap[_now] = temp;

                _now = _next;
            }
        }

        public T Pop()
        {
            if (heap.Count == 0)
                throw new Exception("꺼낼 데이터가 없습니다.");
            //반환할 데이터
            T _returnValue = heap[0];

            //마지막 데이터를 루트로 이동
            int _lastIndex = heap.Count - 1;
            heap[0] = heap[_lastIndex];
            heap.RemoveAt(_lastIndex); //마지막 데이터 삭제

            _lastIndex--;
            int _now = 0;
            while (true)
            {
                int _left = (_now * 2) + 1;
                int _right = (_now * 2) + 2;
                int _next = _now;
                //왼쪽값이 현재값보다 크면, 왼쪽으로 이동
                if (_left <= _lastIndex && heap[_left].CompareTo(heap[_now]) > 0)
                    _next = _left;

                //오른값이 현재값(왼쪽 이동 포함) 보다 크면, 오른쪽으로 이동
                if (_right <= _lastIndex && heap[_right].CompareTo(heap[_next]) > 0)
                    _next = _right;

                if (_next == _now)
                    break;

                T temp = heap[_next];
                heap[_next] = heap[_now];
                heap[_now] = temp;
            
                _now = _next;
            }
            return _returnValue;
        }

        public int Count { get { return heap.Count; } }

    }
}
