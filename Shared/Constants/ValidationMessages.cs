/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

namespace Shared.Constants
{
    public class ValidationMessages
    {
        public static string PatientIsNotExist(int id) => $"Patient with id: {id} is not exist";

        public static string PatientHistoryIsNotExist(int id) => $"Patient History with id: {id} is not exist";

        public static string PatientMovementIsNotExist(int id) => $"Patient Movement with id: {id} is not exist";
    }
}
