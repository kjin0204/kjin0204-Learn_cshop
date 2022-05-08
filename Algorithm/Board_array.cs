using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{

    class MyList<T>
    {
        const int DEFAULTSize = 1;
        T[] _data = new T[DEFAULTSize];

        public int count = 0; //사용중인 데이터 개수
        public int capacity { get { return _data.Length; } } // 예약된 데이터 개수

        //상수 시간복잡도 O{1}
        public void Add(T item)
        {
            //이 부분은 예외 케이스로 복잡도 계산에 넣지 않음.
            if(count >= capacity)
            {
                T[] newArray = new T[count * 2];
                for (int i = 0; i < count; i++)
                    newArray[i] = _data[i];
                _data = newArray;
            }

            _data[count] = item;
            count++;
        }

        //O{1}
        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        //O{n}
        public void RemoveAt(int index)
        {
            for (int i = index; i < count - 1; i++)
                _data[i] = _data[i + 1];
            _data[count - 1] = default(T); //해당 데이터 타입의 디펄트 값 넣음 참조타입이면 null 입력
            count--;
        }
    }

    class Board_array
    {

        public int[] _data = new int[25]; //배열
        //public List<int> _data2 = new List<int>(); //동적 배열
        public MyList<int> _data2 = new MyList<int>(); //동적 배열
        public LinkedList<int> _data3 = new LinkedList<int>(); //연결 리스트


        public void Initialize()
        {
            _data2.Add(101);
            _data2.Add(102);
            _data2.Add(103);
            _data2.Add(104);
            _data2.Add(105);

            int temp = _data2[2];

            _data2.RemoveAt(2);
        }

    }
}
