**Description**

A library for validating and extracting information from a CNP (Romanian national ID)

**Sample usage**

    using alfaNET.Identity.Romania.Cnp;
    
    var cnp = new PersonalNumericCode(1800101420010);
    
    var validationErrors = cnp.Validate();

`Validate` will return `None` if it is valid or a list of errors as a flagged enum.

    var county = cnp.GetCounty(); // Romanian county where the person was born
    var date = cnp.GetDate(); // birth date
    var sex = cnp.GetSex(); // sex of the person
    var seqNo = cnp.GetSequentialNumber(); // the sequential number of birth in that day for that county


**Other aspects**

Validation rules have been modeled after https://github.com/vimishor/cnp-spec/blob/master/spec.md

