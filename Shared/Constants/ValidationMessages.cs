/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

namespace Shared.Constants
{
    public class ValidationMessages
    {
        public const string PatientHasOneOrMoreMovements = "The Patient has one or more visits, please delete them first";

        public const string PatientHasMedicalHistory = "The Patient has medical history, please delete it first";

        public const string AcuteKidneyInjuryRiskAssessmentIsExist = $"The Acute Kidney Injury Risk Assessment is exist";

        public static string PatientIsNotExist(int id) => $"Patient with id: {id} is not exist";

        public static string PatientIsExist(string identity) => $"Patient with identity: {identity} is already exist";

        public static string TeamIsExist(string name) => $"Team with name: {name} is already exist";

        public static string PhoneNumberIsExist(string name) => $"Team Member with phone number: {name} is already exist";

        public static string PatientHistoryIsNotExist(int id) => $"Patient History with id: {id} is not exist";

        public static string PatientMovementIsNotExist(int id) => $"Patient Movement with id: {id} is not exist";

        public static string AcuteKidneyInjuryRiskAssessmentIsNotExist(int id) => $"Acute Kidney Injury Risk Assessment with id: {id} is not exist";
    }
}
