using AutoMapper;
using DevIO.AppMvc.ViewModels;
using DevIO.Business.Models.Fornecedores;
using DevIO.Business.Models.Produtos;
using DevIO.Business.Models.Produtos.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DevIO.AppMvc.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository,
                                  IProdutoService produtoService,
                                  IFornecedorRepository fornecedorRepository,
                                  IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [Route("lista-de-produtos")]
        [HttpGet]
        public async Task<ActionResult> Index() =>
            View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()));

        [Route("dados-do-produto/{id:guid}")]
        public async Task<ActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
                return HttpNotFound();

            return View(produtoViewModel);
        }

        [Route("novo-produto")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var produtoViewModel = await PopularFornecedores(new ProdutoViewModel());
            return View(produtoViewModel);
        }

        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularFornecedores(produtoViewModel);
            if (!ModelState.IsValid) return View(produtoViewModel);

            var imgPrefixo = $"{Guid.NewGuid()}_";
            if (!UploadImagens(produtoViewModel.ImagemUpload, imgPrefixo))
                return View(produtoViewModel);

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return RedirectToAction("Index");
        }

        [Route("editar-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
                return HttpNotFound();

            return View(produtoViewModel);
        }

        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return View(produtoViewModel);

            var produtoAtualizacao = await ObterProduto(produtoViewModel.Id);
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;

            if (produtoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = $"{Guid.NewGuid()}_";
                if (!UploadImagens(produtoViewModel.ImagemUpload, imgPrefixo))
                    return View(produtoViewModel);

                produtoAtualizacao.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;
            produtoAtualizacao.FornecedorId = produtoViewModel.FornecedorId;
            produtoAtualizacao.Fornecedor = produtoViewModel.Fornecedor;

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));
            return RedirectToAction("Index");
        }

        [Route("excluir-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
                return HttpNotFound();

            return View(produtoViewModel);
        }

        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
                return HttpNotFound();

            await _produtoRepository.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private bool UploadImagens(HttpPostedFileBase img, string imgPrefixo)
        {
            if (img == null || img.ContentLength <= 0)
            {
                ModelState.AddModelError(string.Empty, "Imagem em formato inválido!");
                return false;
            }

            var path = Path.Combine(HttpContext.Server.MapPath("~/imagens"), imgPrefixo + img.FileName);
            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            img.SaveAs(path);
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _produtoRepository?.Dispose();
                _produtoService?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}