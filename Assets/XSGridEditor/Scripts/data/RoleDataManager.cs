// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using XSSLG;

// namespace XSSLG 
// {
// 	public class RoleDataManager 
// 	{
// 		RoleDataArray mItemArray;
// 		Dictionary<Int64, int> mKeyIndexMap = new Dictionary<Int64, int>();

// 		static readonly RoleDataManager msInstance = new RoleDataManager();
// 		public static RoleDataManager Instance { get { return msInstance; } }

// 		internal RoleDataManager() 
// 		{
// 			var textAsset = Resources.Load<TextAsset>("data/RoleData");
// 			mItemArray = RoleDataArray.Deserialize(textAsset.bytes);
// 			for (var i = 0; i < mItemArray.Keys.Count; i++)
// 				mKeyIndexMap[mItemArray.Keys[i]] = i;
// 		}

// 		/// <summary>
// 		/// give the key(s) to get item.
// 		/// </summary>
// 		/// <param name="id">角色ID</param> 
// 		public RoleData GetItem(Int64 id) 
// 		{

// 			var _id = ((Int64)(id) << 0);
// 			var _index = 0;
// 			if (mKeyIndexMap.TryGetValue(_id, out _index))
// 				return mItemArray.Items[_index];
// 			return null;
// 		}
// 	}
// }
