using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using VerificationDocs.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using VerificationDocs.Domain.Abstract;
using VerificationDocs.Domain.Concrete;
using VerificationDocs.WebUI.Infrastructure.Abstract;
using VerificationDocs.WebUI.Infrastructure.Concrete;

namespace VerificationDocs.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //// Заполнение тестового имитированного хранилища
            //Mock<IStyleRepository> mock = new Mock<IStyleRepository>();
            //mock.Setup(m => m.Styles).Returns(new List<StyleP>
            //{
            //    new StyleP {ParagraphID=0, Name="Заголовок", Font="Times New Roman", Color = "0", Inscription="Жирный", Size="14", Alignment="По центру", LeftIndention=2.5, RightIndention=2.5},
            //    new StyleP {ParagraphID=1, Name="Обычный текст", Font="Times New Roman", Color = "0", Inscription="Обычный", Size="12", Alignment="По ширине", LeftIndention=2.5, RightIndention=2.5},
            //    new StyleP {ParagraphID=2, Name="Обычный текст", Font="Times New Roman", Color = "0", Inscription="Обычный", Size="12", Alignment="По ширине", LeftIndention=2.5, RightIndention=2.5},

            //});
            //kernel.Bind<IStyleRepository>().ToConstant(mock.Object);

            // Из БД
            kernel.Bind<IStyleRepository>().To<EFStyleRepository>();

            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}