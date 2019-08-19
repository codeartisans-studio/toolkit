using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    public static class WindowsManager
    {
        private static List<GameObject> boards = new List<GameObject>();

        public static void Add(GameObject board)
        {
            boards.Add(board);
        }

        public static void Remove(GameObject board)
        {
            boards.Remove(board);
        }

        public static void Open(GameObject board)
        {
            board.SetActive(true);
        }

        public static void Close(GameObject board)
        {
            board.SetActive(false);
        }

        public static void CloseAll()
        {
            foreach (GameObject board in boards)
            {
                Close(board);
            }
        }
    }
}
