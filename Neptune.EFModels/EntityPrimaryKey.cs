/*-----------------------------------------------------------------------
<copyright file="LtInfoEntityPrimaryKey.cs" company="Sitka Technology Group">
Copyright (c) Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Globalization;
using Neptune.Common;

namespace Neptune.EFModels
{
    /// <summary>
    /// Provides a way to have the MVC controller action context bind and load the object from the database
    /// </summary>
    public class EntityPrimaryKey<T> where T : class, IHavePrimaryKey
    {
        private int _primaryKeyValue;
        public EntityPrimaryKey()
        {

        }
        public EntityPrimaryKey(int primaryKeyValue)
        {
            _primaryKeyValue = primaryKeyValue;
            // This isn't solved but I'm not sure we need it
            // _entityObject = new Lazy<T>(() => (T)SitkaHttpRequestStorage.LtInfoEntityTypeLoader.LoadType(typeof(T), primaryKeyValue));
        }
        public EntityPrimaryKey(T theObject)
        {
            _primaryKeyValue = theObject.PrimaryKey;
            _entityObject = new Lazy<T>(() => theObject);
        }
        private Lazy<T> _entityObject;
        public T EntityObject
        {
            get => _entityObject?.Value;
            set => _entityObject = new Lazy<T>(value);
        }
        public int PrimaryKeyValue
        {
            get => _entityObject.IsValueCreated ? _entityObject.Value.PrimaryKey : _primaryKeyValue;
            set => _primaryKeyValue = value;
        }
        public override string ToString()
        {
            return PrimaryKeyValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}
