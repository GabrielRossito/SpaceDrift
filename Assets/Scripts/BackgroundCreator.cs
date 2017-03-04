using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class BackgroundCreator : MonoBehaviour
{
    private Transform _refBacgroundBase { get { return transform.GetChild(0); } }

    private List<Transform> _backgrounds = new List<Transform>();

    private Transform HigherTransform { get { return _backgrounds.LastOrDefault(); } }

    private Transform MiddleTransform { get { return _backgrounds[2]; } }

    private Transform LowerTransform { get { return _backgrounds[0]; } }

    public void Initialize()
    {
        CreateBackground(4);
    }

    public void ValidatePositions(Vector3 playerPosition)
    {
        Bounds middleBkBounds = MiddleTransform.GetComponent<Renderer>().bounds;
        float limit = MiddleTransform.position.y + middleBkBounds.extents.y;
        if ((playerPosition.y - limit) > 0.1)
            ChangePositions();
    }

    private void CreateBackground(int size)
    {
        _backgrounds = new List<Transform>();
        _backgrounds.Add(_refBacgroundBase);
        _refBacgroundBase.name = "0";

        for (int i = 1; i < size; i++)
        {
            Transform newBk = Instantiate(_refBacgroundBase, transform) as Transform;
            newBk.name = i.ToString();
            Vector3 pos = _backgrounds[i - 1].GetComponent<Renderer>().bounds.max + _backgrounds[i - 1].GetComponent<Renderer>().bounds.extents;
            pos.z = pos.x = 0;
            newBk.transform.localPosition = pos;
            _backgrounds.Add(newBk.transform);
        }
    }

    private void ChangePositions()
    {
        Vector3 pos = HigherTransform.GetComponent<Renderer>().bounds.max + HigherTransform.GetComponent<Renderer>().bounds.extents;
        pos.z = pos.x = 0;
        LowerTransform.localPosition = pos;

        //LowerTransform = _backgrounds[0];
        //HigherTransform = _backgrounds[0];
        //for (int i = 0; i < _backgrounds.Count; i++)
        //{
        //    if (_backgrounds[i].position.y < LowerTransform.position.y)
        //        LowerTransform = _backgrounds[i];
        //    if (_backgrounds[i].position.y > HigherTransform.position.y)
        //        HigherTransform = _backgrounds[i];
        //}

        //List<Transform> ordered = _backgrounds.OrderBy(x => x.position.y).ToList();
        QuickSortTransforms(_backgrounds, 0, _backgrounds.Count - 1);
        //LowerTransform = _backgrounds[0];
        //HigherTransform = _backgrounds[_backgrounds.Count];
        //MiddleTransform = _backgrounds[_backgrounds.Count / 2];
        //MiddleTransform = ordered[ordered.Count / 2];
    }

    private void QuickSortTransforms(List<Transform> elements, int left, int right)
    {
        int i = left, j = right;
        Transform pivot = _backgrounds[(left + right) / 2];

        while (i <= j)
        {
            while (i < j && elements[i].position.y < pivot.position.y) //(elements[i].CompareTo(pivot) < 0)
            {
                i++;
            }

            while (i < j && elements[j].position.y > pivot.position.y)//(elements[j].CompareTo(pivot) > 0)
            {
                j--;
            }

            if (i <= j)
            {
                // Swap
                Transform tmp = elements[i];
                elements[i] = elements[j];
                elements[j] = tmp;

                i++;
                j--;
            }
        }

        // Recursive calls
        if (left < j)
        {
            QuickSortTransforms(elements, left, j);
        }

        if (i < right)
        {
            QuickSortTransforms(elements, i, right);
        }
    }
}