using App.Domain;
using Base.Contracts.DAL;

namespace App.DAL.EF;

public class DalDummyMapper<TLeftObject, TRightObject> : IDalMapper<TLeftObject, TRightObject>
    where TLeftObject : class where TRightObject : class
{
    
    public TLeftObject? MapRL(TRightObject? inObject)
    {
        return inObject as TLeftObject;
    }

    public TRightObject? MapLR(TLeftObject? inObject)
    {
        return inObject as TRightObject;
    }
}