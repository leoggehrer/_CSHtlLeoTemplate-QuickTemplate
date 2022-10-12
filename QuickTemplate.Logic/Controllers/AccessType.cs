﻿//@CodeCopy
//MdStart

namespace QuickTemplate.Logic.Controllers
{
    public enum AccessType
    {
        GetBy = 1,
        GetAll = 2 * GetBy,
        GetPageList = 2 * GetAll,

        QueryCount = 2 * GetPageList,
        QueryCountBy = 2 * QueryCount,

        QueryBy = 2 * QueryCountBy,
        QueryAll = 2 * QueryBy,

        Create = 2 * QueryAll,
        Insert = 2 * Create,
        Update = 2 * Insert,
        Delete = 2 * Update,

        InsertArray = 2 * Delete,
        UpdateArray = 2 * InsertArray,
        
        Save = 2 * UpdateArray,
        Reject = 2 * Save,

        Logout = 2 * Reject,

        Get = GetBy + GetAll,
        Query = QueryBy + QueryAll,
    }
}
//MdEnd
