using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;
using System.ComponentModel;
using System.Numerics;

namespace Neo3Contract
{
    [DisplayName("MyTestContract")]
    [ManifestExtra("Author", "ProDog")]
    [ManifestExtra("Email", "xxxxxxx")]
    [ManifestExtra("Description", "This is a Neo3 contract example")]
    [SupportedStandards("NEP", "NEP-11")]
    [ContractPermission("*", "*")]
    public partial class Nep11Contract : Nep11Token<Nep11TokenState>
    {
        [InitialValue("NUNtEBBbJkmPrmhiVSPN6JuM7AcE8FJ5sE", Neo.SmartContract.ContractParameterType.Hash160)]
        public static UInt160 Owner;

        public delegate void Notify(params object[] arg);

        [DisplayName("event_name")]
        public static event Notify OnNotify;

        public override string Symbol()
        {
            return "Neo-Nep11";
        }

        public static bool Verify()
        {
            return Runtime.CheckWitness(Owner);
        }

        public static void _deploy(object data, bool update)
        {
            if (update)
            {
                Storage.Put(Storage.CurrentContext, "update", 111213);
                OnNotify("update", 1);
            }
            else
            {
                Storage.Put(Storage.CurrentContext, "deploy", 111213);
                OnNotify("deploy", 1);
            }
        }

        public static bool Update(ByteString nef, string manifest)
        {
            if (!Runtime.CheckWitness(Owner)) return false;
            ContractManagement.Update(nef, manifest, null);
            return true;
        }

        public static void Mint(UInt160 to, ByteString name)
        {
            ByteString tokenID = NewTokenId();
            Nep11TokenState tokenState = new Nep11TokenState() { Owner = to, Name = name };

            Nep11Token<Nep11TokenState>.Mint(tokenID, tokenState);
        }

        public static void Burn(ByteString tokenID)
        {

            Nep11Token<Nep11TokenState>.Burn(tokenID);
        }

        public static void OnNEP11Payment(UInt160 from, BigInteger amount, ByteString tokenId, object data)
        {
            OnNotify("OnNEP11Payment", from, amount, tokenId, data);

            Mint(from, "OnNEP11Payment");
        }

        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
        {
            OnNotify("OnNEP17Payment", from, amount, data);

            Mint(from, "OnNEP17Payment");
        }
       
    }
}
