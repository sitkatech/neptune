﻿/*-----------------------------------------------------------------------
<copyright file="MergeListHelper.cs" company="Sitka Technology Group">
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

using Microsoft.EntityFrameworkCore;

namespace Neptune.Common
{
    public static class MergeListHelper
    {
        public delegate bool Match<in T>(T o1, T o2);
        public delegate void UpdateFunction<in T>(T o1, T o2);

        public static void Merge<T>(this ICollection<T> existingList, ICollection<T> updatedList, DbSet<T> allInDatabase, Match<T> matchCriteria) where T : class
        {
            existingList.Merge(updatedList, allInDatabase, matchCriteria, null);
        }

        public static void Merge<T>(this ICollection<T> existingList, ICollection<T> updatedList, DbSet<T> allInDatabase, Match<T> matchCriteria, UpdateFunction<T> updateFunction) where T : class
        {
            existingList.MergeNew(updatedList, allInDatabase, matchCriteria);
            if (updateFunction != null)
            {
                existingList.MergeUpdate(updatedList, matchCriteria, updateFunction);
            }
            existingList.MergeDelete(updatedList, matchCriteria, allInDatabase);
        }

        /// <summary>
        /// Like Merge but without deleting. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="existingList"></param>
        /// <param name="updatedList"></param>
        /// <param name="allInDatabase"></param>
        /// <param name="matchCriteria"></param>
        /// <param name="updateFunction"></param>
        public static void MergeUpsert<T>(this ICollection<T> existingList, ICollection<T> updatedList, DbSet<T> allInDatabase, Match<T> matchCriteria, UpdateFunction<T> updateFunction) where T : class
        {
            existingList.MergeNew(updatedList, allInDatabase, matchCriteria);
            if (updateFunction != null)
            {
                existingList.MergeUpdate(updatedList, matchCriteria, updateFunction);
            }
        }


        public static void MergeNew<T>(this ICollection<T> existingList, IEnumerable<T> updatedList,
            DbSet<T> allInDatabase, Match<T> matchCriteria) where T : class
        {
            // Inserting new records
            foreach (var currentRecordFromForm in updatedList)
            {
                var existingRecord = existingList.MatchRecord(currentRecordFromForm, matchCriteria);
                if (Equals(existingRecord, default(T)))
                {
                    existingList.Add(currentRecordFromForm);
                    allInDatabase.Add(currentRecordFromForm);
                }
            }
        }

        public static void MergeUpdate<T>(this ICollection<T> existingList, IEnumerable<T> updatedList, Match<T> matchCriteria, UpdateFunction<T> updateFunction) where T : class
        {
            foreach (var currentRecordFromForm in updatedList)
            {
                var existingRecord = existingList.MatchRecord(currentRecordFromForm, matchCriteria);
                if (!Equals(existingRecord, default(T)))
                {
                    updateFunction(existingRecord, currentRecordFromForm);
                }
            }
        }

        public static void MergeDelete<T>(this ICollection<T> existingList, IEnumerable<T> updatedList, Match<T> matchCriteria, DbSet<T> allInDatabase) where T : class
        {
            // Deleting records from existing that are no longer in fromForm
            var recordsToDelete = existingList.Where(existingRecord => Equals(updatedList.MatchRecord(existingRecord, matchCriteria), default(T))).ToList();
            recordsToDelete.ForEach(recordToDelete =>
            {
                allInDatabase.Remove(recordToDelete);
                existingList.Remove(recordToDelete);
            });
        }

        private static T MatchRecord<T>(this IEnumerable<T> listToSearch, T itemToSearch, Match<T> matcher)
        {
            if (matcher == null)
            {
                return default;
            }
            return listToSearch.SingleOrDefault(x => matcher(itemToSearch, x));
        }
    }
}
