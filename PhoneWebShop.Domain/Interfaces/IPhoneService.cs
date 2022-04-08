using PhoneWebShop.Domain.Events;
using PhoneWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneWebShop.Domain.Interfaces
{
    /// <summary>
    /// A class for keeping track of all Phone instances
    /// </summary>
    public interface IPhoneService
    {
        /// <summary>
        /// Return the first phone that matches with the parameters.
        /// </summary>
        /// <param name="id">The Id of the phone to return</param>
        /// <returns>The phone with Id. If no phone was found, returns null</returns>
        Task<Phone> GetAsync(int id);

        /// <summary>
        /// Adds a new Phone to the db.
        /// </summary>
        /// <param name="phone">The object to add to the db.</param>
        /// <returns>Whether the action was successful.</returns>
        Task<bool> AddAsync(Phone phone);

        /// <summary>
        /// Fetch a list of all db entries for phones.
        /// </summary>
        /// <returns>A new IEnumerable<Phone> with all phone instances in it. Ordered ASC</returns>
        Task<IEnumerable<Phone>> GetAll();

        /// <summary>
        /// Update a phone in the database. Will use ID of the phone that was parsed as parameter.
        /// </summary>
        /// <param name="phone">The phone object containing the new values.</param>
        Task UpdateAsync(Phone phone);

        /// <summary>
        /// Given a query, returns an IEnumerable of phones that match the query.
        /// </summary>
        /// <param name="query">The search term</param>
        /// <returns>A new list object containing all matches</returns>
        Task<IEnumerable<Phone>> Search(string query);

        /// <summary>
        /// Deletes the phone with the matching id.
        /// </summary>
        /// <param name="id">Id of the phone that's being deleted.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Checks whether a phone exists in the database.
        /// </summary>
        /// <param name="phone">The phone to check for</param>
        /// <returns>true if the phone exists, false if not.</returns>
        //bool Exists(Phone phone);
    }
}
