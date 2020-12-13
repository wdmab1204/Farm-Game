/*
<copyright file="BGRotatable.cs" company="BansheeGz">
    Copyright (c) 2018-2020 All Rights Reserved
</copyright>
*/

using UnityEngine;

namespace BansheeGz.BGDatabase.Example
{

	public partial class BGRotatable : MonoBehaviour
	{
		void Update()
		{
			transform.Rotate(Vector3.up * Time.deltaTime * 50);
		}
	}
}