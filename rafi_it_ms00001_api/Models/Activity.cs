using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.Models
{
    [DataContract]
    public class Activity
    {
        // <summary>
        //I have added Name property to the DataMember attribute.
        //This is because this name will be the key in the JSON returned wherever this model is used.
        //This practice helps in hiding our actual property names to the outside exposure.
        //The major point is if in future you have decided to rename your properties,
        //then you can do that but you don’t have to change the name property value
        //and hence consumer of this model won’t notice any difference but
        //if you rename your property without the name attribute then
        //the consumer of this model will have to modify his/her code also.
        // </summary>

        // @referrence: https://www.dotnetperls.com/property
        // @referrence: https://stackify.com/asp-net-core-testing-tools/
        string _SystemName;
        string _ActionName;
        string _UserName;
        string _Remarks;
        DateTime _DateCreated;

        /// <summary>
        /// The Activity  on the table
        /// </summary>
        /// <example> 0192820</example>
        [Required(ErrorMessage = "Activity Id is required.")]
        [DataMember(Name = "ActivityID")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Invalid Activity Id.")]
        public long ActivityId { get; set; }

        /// <summary>
        /// The System name should define from the application using this API
        /// </summary>
        /// <example>BranchInfo System</example>
        [Required(ErrorMessage = "System Name is required.")]
        [StringLength(60, ErrorMessage = "System Name can not be longer than 60 characters.")]
        [DataMember(Name = "SystemName")]
        [RegularExpression("^[a-zA-Z0-9]+(?:[ _][a-zA-Z0-9]+)?$", ErrorMessage = "Invalid System Name.")]
        public string SystemName
        {
            get
            {
                return this._SystemName;
            }
            set
            {
                this._SystemName = value;
            }
        }

        /// <summary>
        /// The Action name should expect data related action in the navigation
        /// by the user on the applicaiton using this API
        /// </summary>
        /// <example>Add, Edit, Delete, Open, Logout</example>
        [Required(ErrorMessage = "Action name is required.")]
        [StringLength(60, ErrorMessage = "Action Name can not be longer than 60 characters.")]
        [DataMember(Name = "ActionName")]
        [RegularExpression("^[a-zA-Z0-9]+(?:[ _][a-zA-Z0-9]+)?$", ErrorMessage = "Invalid Action Name.")]
        public string ActionName
        {
            get
            {
                return this._ActionName;
            }
            set
            {
                this._ActionName = value;
            }
        }

        /// <summary>
        /// The User Name should be based on the account name logging in the system/application
        /// </summary>
        /// <example>francisco.abayon@rafi.ph</example>
        [Required(ErrorMessage = "User Name is required.")]
        [StringLength(70, ErrorMessage = "User Name can not be longer than 70 characters.")]
        [DataMember(Name = "UserName")]
        [RegularExpression("^[a-zA-Z0-9]+(?:[ _][a-zA-Z0-9]+)?$", ErrorMessage = "Invalid User Name.")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }


        /// <summary>
        /// The remarks is the data newly entry,deleted,updated or selected
        /// </summary>
        /// <example>{activityid:12391, name:sample name, age:jakaidf}</example>
        [Required(ErrorMessage = "Remarks is required.")]
        [StringLength(350, ErrorMessage = "Remarks can not be longer than 350 characters.")]
        [DataMember(Name = "Remarks")]

        //@todo: Add regex that can accept space,
        // json format(semicolon,colon,curly bracket and angle bar) and alphanumeric(uppercase and lowercase)
        //[RegularExpression("^[a-zA-Z0-9]+(?:[ _][a-zA-Z0-9]+)?$", ErrorMessage = "Invalid Action Name.")]
        public string Remarks
        {
            get
            {
                return this._Remarks;
            }
            set
            {
                this._Remarks = value;
            }
        }

        /// <summary>
        /// The Datecreated is the for when it is entry
        /// </summary>
        /// <example>2019-12-25</example>
        [Required(ErrorMessage = "Date created is required.")]
        [DataMember(Name = "DateCreated")]

        //@todo: Add regex that can accept space,
        // json format(semicolon,colon,curly bracket and angle bar) and alphanumeric(uppercase and lowercase)
        //[RegularExpression("^[a-zA-Z0-9]+(?:[ _][a-zA-Z0-9]+)?$", ErrorMessage = "Invalid Action Name.")]
        public DateTime DateCreated
        {
            get
            {
                return this._DateCreated;
            }
            set
            {
                this._DateCreated = value;
            }
        }
    }
}
