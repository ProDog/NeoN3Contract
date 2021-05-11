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
    [ManifestExtra("Email", "xxxxxx")]
    [ManifestExtra("Description", "This is a Neo3 contract example")]
    [SupportedStandards("NEP", "null")]
    [ContractPermission("*", "*")]
    public partial class Nep17Contract : Nep17Token
    {
        [InitialValue("NUNtEBBbJkmPrmhiVSPN6JuM7AcE8FJ5sE", Neo.SmartContract.ContractParameterType.Hash160)]
        public static UInt160 Owner;

        public override string Symbol()
        {
            return "Neo-Nep17";
        }

        public override byte Decimals()
        {
            return 8;
        }

        public static bool Verify()
        {
            return Runtime.CheckWitness(Owner);
        }

        public delegate void Notify(params object[] arg);

        [DisplayName("test_event")]
        public static event Notify OnNotify;

        public static void _deploy(object data, bool update)
        {
            if (!update)
            {
                Mint(Owner, 1_0000_0000_00000000);
                Storage.Put(Storage.CurrentContext, "deploy", 12345);
                OnNotify("deploy", 1);
            }
            else
            {
                Storage.Put(Storage.CurrentContext, "update", 11);
                OnNotify("update", 1);
            }
        }

        public static bool Update(ByteString nef, string manifest)
        {
            if (!Runtime.CheckWitness(Owner)) return false;
            ContractManagement.Update(nef, manifest, null);
            return true;
        }

        public static new void Mint(UInt160 to, BigInteger amt)
        {
            Nep17Token.Mint(to, amt);
        }

        public static new void Burn(UInt160 account, BigInteger amt)
        {
            Nep17Token.Burn(account, amt);
        }

        public static UInt160 Test(int amt)
        {
            var tx = (Transaction)Runtime.ScriptContainer;

            return tx.Sender;
        }

        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
        {

        }
    }
}
