namespace gha.mobile.common.repositories
{
    public static class ErrorChecker
    {
        public static EpicorStatus CheckError(dynamic epicorResponse)
        {
            if (epicorResponse.ContainsKey("ErrorMessage"))
            {
                return (string)epicorResponse.ErrorMessage switch
                {
                    "Could not find a property named 'GHA_MIMS_SequenceNo_c' on type 'Erp.Warehse'." => EpicorStatus.Epicor_Warehouse_UD_Does_Not_Exist,
                    "Could not find a property named 'GHA_MIMS_PickLoc_c' on type 'Erp.Warehse'." => EpicorStatus.Epicor_Warehouse_UD_Does_Not_Exist,
                    "Cannot return more than was issued." => EpicorStatus.Epicor_Cannot_Return_More_Than_Issued,
                    "There is not enough unallocated inventory in this bin for this transaction." => EpicorStatus.Epicor_Not_Enough_Unallocated_Inventory,
                    _ => EpicorStatus.Epicor_Unknown_Error
                };
            }
            else
                return EpicorStatus.Epicor_Ok;
        }

        public static string GetErrorMessage(EpicorStatus status)
        {
            return status switch
            { 
                EpicorStatus.Epicor_Cannot_Return_More_Than_Issued => "Cannot return more than was issued.",
                EpicorStatus.Epicor_Not_Enough_Unallocated_Inventory => "There is not enough unallocated inventory in this bin for this transaction.",
                _ => "Unknown error."
            };
        }
    }
}