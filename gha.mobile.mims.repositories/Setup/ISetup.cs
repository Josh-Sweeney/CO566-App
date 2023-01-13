using gha.mobile.common.entities;

namespace gha.mobile.mims.repositories.Setup
{
    public interface ISetup
    {
        EpicorResponse TestConnection(ERPConnection erpConnection);
    }
}